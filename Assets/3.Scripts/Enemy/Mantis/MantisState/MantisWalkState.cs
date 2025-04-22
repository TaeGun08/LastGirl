using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MantisWalkState : EnemyState
{
    private static readonly int WALK = Animator.StringToHash("Walk");
    public override StateName Name =>  StateName.Walk;

    private Tween tween;
    private bool isRotating;
    
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
        this.enemy.Animator.SetTrigger(WALK);
    }

    public override void StateUpdate()
    {
        float distance = Vector3.Distance(enemy.LocalPlayer.transform.position, transform.position);
        enemy.Agent.SetDestination(enemy.LocalPlayer.transform.position);
            
        Vector3 direction = (enemy.LocalPlayer.transform.position - transform.position).normalized;
        float target = Vector3.Angle(transform.forward, direction);
        
        if (distance > 12f || target > enemy.chaseAngle * 0.5f) return;
        enemy.ChangeState(StateName.Idle);
    }

    public override void StateExit()
    {
        enemy.Animator.ResetTrigger(WALK);
        gameObject.SetActive(false);
    }
    
}
