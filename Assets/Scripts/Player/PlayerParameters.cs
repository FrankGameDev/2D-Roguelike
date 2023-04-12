using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerParams", menuName = "MyObject/PlayerParams")]
public class PlayerParameters : GenericStats
{
    [Header("Movement fields")]
    public float speed;
    public float dodgeCooldown;

    [Header("Attack fields")]
    [Header("------------------")]
    [Header("Attack hit parameters")]
    public float attackRadius;
    public LayerMask enemyMask;
    [Header("------------------")]
    public float attackDmg;
    public float attackCooldown;

}
