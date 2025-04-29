using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScolopendraIdleState : EnemyState
{
    private static readonly int IDLE = Animator.StringToHash("Idle");

    public override StateName Name => StateName.Idle;

    private void OnDrawGizmos()
    {
        if(enemy == null) return;
        Vector3 leftBoundary = Quaternion.Euler(0, -enemy.chaseAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, enemy.chaseAngle / 2, 0) * transform.forward;
        
        if (enemy.chaseAngle <= 180f)
        {
            Gizmos.color = Color.white;
        
            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * 5);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * 5);
        }
        else
        {
            Gizmos.color = Color.red;
        
            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * 5);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * 5);
        }
    }
    
    public override void StateEnter(Enemy enemy)
    {
        this.enemy = enemy;
        this.enemy.Animator.SetTrigger(IDLE);
    }

    public override void StateUpdate()
    {
        Vector3 direction = (enemy.LocalPlayer.transform.position - enemy.transform.position).normalized;
        float target = Vector3.Angle(transform.forward, direction);
        
        float distance = Vector3.Distance(new Vector3(enemy.transform.position.x, 0f, enemy.transform.position.z), 
            new Vector3(enemy.LocalPlayer.transform.position.x, 0f, enemy.LocalPlayer.transform.position.z));

        if (target <= enemy.chaseAngle * 0.5f && distance <= 11) return;
        enemy.ChangeState(StateName.Walk);
    }

    public override void StateExit()
    {
        enemy.Animator.ResetTrigger(IDLE);
        gameObject.SetActive(false);
    }
}