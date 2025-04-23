using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScolopendraAttackState : EnemyState
{
    private static readonly int PATTERN = Animator.StringToHash("Pattern");
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    
    public override StateName Name => StateName.Attack;
    
    [Header("Scolopendra Settings")] 
    [SerializeField] private EnemyAttack[] enemyAttacks;
    
    public override void StateEnter(Enemy enemy)
    {
        this.enemy = enemy;
        this.enemy.isPattern = true;
        int randomPattern = Random.Range(0, enemy.hasPattern);
        Vector3 direction = (enemy.LocalPlayer.transform.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        enemy.transform.DORotateQuaternion(lookRotation, 0.5f);
        this.enemy.Animator.SetTrigger(ATTACK);
        this.enemy.Animator.SetFloat(PATTERN, randomPattern);
        StartCoroutine(enemyAttacks[randomPattern].Pattern(this.enemy));
    }

    public override void StateUpdate()
    {
        if (enemy.isPattern) return;
        enemy.ChangeState(StateName.Idle);
    }

    public override void StateExit()
    {
        enemy.Animator.ResetTrigger(ATTACK);
        gameObject.SetActive(false);
    }
}
