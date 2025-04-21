using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    private static readonly int DEAD = Animator.StringToHash("Dead");
    public override StateName Name => StateName.Dead;
    
    public override void StateEnter(PlayerController playerController)
    {
        this.playerController = playerController;
        animator.SetTrigger(DEAD);
    }

    public override void StateUpdate()
    {

    }

    public override void StateExit()
    {

    }
}
