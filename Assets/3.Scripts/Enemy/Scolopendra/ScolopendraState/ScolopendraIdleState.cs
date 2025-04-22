using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScolopendraIdleState : EnemyState
{
    private static readonly int IDLE = Animator.StringToHash("Idle");

    public override StateName Name => StateName.Idle;

    public override void StateEnter(Enemy enemy)
    {
        this.enemy = enemy;
        this.enemy.Animator.SetTrigger(IDLE);
    }

    public override void StateUpdate()
    {
        Vector3 direction = (enemy.LocalPlayer.transform.position - enemy.transform.position).normalized;
        float target = Vector3.Angle(transform.forward, direction);

        if (target <= enemy.chaseAngle * 0.5f) return;
        enemy.ChangeState(StateName.Walk);
    }

    public override void StateExit()
    {
        enemy.Animator.ResetTrigger(IDLE);
        gameObject.SetActive(false);
    }
}