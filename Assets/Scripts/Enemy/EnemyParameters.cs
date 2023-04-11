using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyParams", menuName = "MyObject/EnemyParams")]
public class EnemyParameters : GenericStats
{
    [Header("Movement fields")]
    public float speed;

    [Header("Attack fields")]
    [Header("------------------")]
    [Header("Attack hit parameters")]
    public float attackRadius;
    [Header("------------------")]
    public float attackDmg;
    public float attackCooldown;
}
