using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class MantisAttackState : EnemyState
{
    private static readonly int PATTERN = Animator.StringToHash("Pattern");
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    public override StateName Name => StateName.Attack;

    [Header("MantisAttackState Settings")] 
    [SerializeField] private EnemyAttack[] enemyAttacks;

    public override void StateEnter(Enemy enemy)
    {
        this.enemy = enemy;
        this.enemy.isPattern = true;
        int randomPattern = Random.Range(0, enemy.hasPattern);
        this.enemy.Animator.SetTrigger(ATTACK);
        this.enemy.Animator.SetFloat(PATTERN, randomPattern);
        StartCoroutine(enemyAttacks[randomPattern].Pattern(this.enemy));
    }

    public override void StateUpdate()
    {
        if (enemy.isPattern) return;
        enemy.ChangeState(StateName.Walk);
    }

    public override void StateExit()
    {
        enemy.Animator.ResetTrigger(ATTACK);
        gameObject.SetActive(false);
    }
}