using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;


    private int _isWalkingHash;
    private int _isDodgingHash;
    private int _takeDamageHash;
    private int _isAttackingHash;
    private int _attackTypeHash;

    private int _randomAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isDodgingHash = Animator.StringToHash("isDodging");
        _isAttackingHash = Animator.StringToHash("isAttacking");
        _attackTypeHash = Animator.StringToHash("attackType");
        _takeDamageHash = Animator.StringToHash("takeDamage");

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
