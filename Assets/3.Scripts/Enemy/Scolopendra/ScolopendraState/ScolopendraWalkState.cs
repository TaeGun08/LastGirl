using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScolopendraWalkState : EnemyState
{
    private static readonly int WALK = Animator.StringToHash("Walk");
    public override StateName Name => StateName.Walk;
    
    public override void StateEnter(Enemy enemy)
    {
        this.enemy = enemy;
        this.enemy.Animator.SetTrigger(WALK);
        StartCoroutine(ChaseCoroutine());
    }

    public override void StateUpdate()
    {
        
    }

    private IEnumerator ChaseCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        enemy.transform.position = enemy.LocalPlayer.transform.position;
        Vector3 direction = (enemy.LocalPlayer.transform.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        enemy.transform.DORotateQuaternion(lookRotation, 1f);
        yield return new WaitForSeconds(3f);
        enemy.ChangeState(StateName.Idle);
    }

    public override void StateExit()
    {
        enemy.Animator.ResetTrigger(WALK);
        gameObject.SetActive(false);
    }
}
