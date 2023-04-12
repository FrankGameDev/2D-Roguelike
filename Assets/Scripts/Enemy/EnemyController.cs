using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]

public class EnemyController : MonoBehaviour, IDamageable, IHealthable
{

    [Header("References")]
    [HideInInspector]
    public Rigidbody2D rb;
    public EnemyParameters enemyParameters;
    private SpriteRenderer rbSprite;


    public float health { get => _hp; set => _hp = value; }

    private float _hp;

    // -- PLAYER REF
    private Transform player;

    // -- MOVEMENT
    private Vector2 _distanceFromPlayer;
    private int _facingDirection;

    // -- ATTACK
    private bool _canAttack = true;

    // -- ANIMATOR
    private Animator animator;

    private int _isWalkingHash;
    private int _isDodgingHash;
    private int _takeDamageHash;
    private int _isAttackingHash;
    private int _attackTypeHash;
    private int _isDeadHash;

    private int _randomAttack;

    // -- EVENTS
    public Action<float> onDamage;

    // -- UTILITY
    private bool _isDisabled;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isAttackingHash = Animator.StringToHash("isAttacking");
        _attackTypeHash = Animator.StringToHash("attackType");
        _takeDamageHash = Animator.StringToHash("takeDamage");
        _isDeadHash = Animator.StringToHash("isDead");

        _hp = enemyParameters.hp;

    }

    // Update is called once per frame
    void Update()
    {
        if (_isDisabled)
            return;

        StartAttack();
    }

    private void FixedUpdate()
    {
        if (_isDisabled)
            return;

        MoveTowardsPlayer();
    }

    #region Movement

    private void GetDirection()
    {
        _distanceFromPlayer = player.position - transform.position;

        _facingDirection = (_distanceFromPlayer.x >= 0) ? 1 : -1;
        rbSprite.flipX = _facingDirection < 0;
    }

    private void MoveTowardsPlayer()
    {
        //TODO aggiungere rallentamento o stop nemico ad una certa distanza

        GetDirection();

        float speed = enemyParameters.speed;

        if (_distanceFromPlayer.magnitude <= 3f)
        {
            Debug.Log("Troppo vicini");
            speed /= 2;
        }

        rb.MovePosition(rb.position + _distanceFromPlayer.normalized * speed * Time.fixedDeltaTime);
        animator.SetBool(_isWalkingHash, _distanceFromPlayer.magnitude > 0f);
    }

    #endregion

    private void StartAttack()
    {
        if (!_canAttack || _distanceFromPlayer.magnitude > enemyParameters.attackRadius)
            return;

        AttackAnimationHandler();
        StartCoroutine(ResetAttack(enemyParameters.attackCooldown));
       
    }

    private void AttackHit()
    {
        Collider2D playerColl = OnHit();
        if (playerColl != null)
            CombatManager.instance.ApplyDmgOutput(playerColl, enemyParameters.attackDmg);
    }

    private Collider2D OnHit()
    {
        Collider2D coll = Physics2D.OverlapCircle((Vector2)transform.position + _distanceFromPlayer.normalized,
          enemyParameters.attackRadius);
        if (coll.CompareTag("Player"))
            return coll;
        return null;
    }

    IEnumerator ResetAttack(float attackCD)
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackCD);
        _canAttack = true;
    }

    private void AttackAnimationHandler()
    {
        _randomAttack = UnityEngine.Random.Range(0, 2);
        animator.SetTrigger(_isAttackingHash);
        animator.SetInteger(_attackTypeHash, _randomAttack);
    }


    private void TakeDamage(float dmgAmount)
    {
        ApplyKnockback();
        health -= dmgAmount;
        if (health <= 0)
        {
            Die();
        }
        StartCoroutine(DisableForSeconds(1f));
        animator.SetTrigger(_takeDamageHash);
    }

    private void Die()
    {
        animator.SetBool(_isDeadHash, true);
        EnemyInactive();
    }


    //DisableEnemyOperations for n seconds
    IEnumerator DisableForSeconds(float seconds)
    {
        _isDisabled = true;
        yield return new WaitForSeconds(seconds);
        _isDisabled = false;
    }

    private void ApplyKnockback()
    {
        rb.AddForce(-1 * _distanceFromPlayer.normalized, ForceMode2D.Impulse);
    }

    /**
     * Applica eventi durante l'azione di prendere danno
     */
    public void ApplyDamage(float dmgAmount)
    {
        onDamage += TakeDamage;
        onDamage?.Invoke(dmgAmount);
    }


    private void EnemyInactive()
    {
        gameObject.SetActive(false);
    }
}
