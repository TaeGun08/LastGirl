using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisDeadState : EnemyState
{
    private static readonly int DEAD = Animator.StringToHash("Dead");
    public override StateName Name => StateName.Dead;
    public override void StateEnter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.Animator.SetTrigger(DEAD);
    }

    public override void StateUpdate()
    {
        
    }

    public override void StateExit()
    {

    }
}
