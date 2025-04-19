using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrowdControlState : PlayerState
{
    private static readonly int CROWD_CONTROL = Animator.StringToHash("CrowdControl");
    public override StateName Name => StateName.CrowdControl;

    private float ccTimer = 0f;
    
    public override void StateEnter()
    {
        animator.SetTrigger(CROWD_CONTROL);
        ccTimer = 2f;
    }

    public override void StateUpdate(PlayerController playerController)
    {
        ccTimer -= Time.deltaTime;
        if (ccTimer > 0) return;
        
        playerController.ChangeState(StateName.Idle);
    }

    public override void StateExit()
    {
        animator.ResetTrigger(CROWD_CONTROL);
        gameObject.SetActive(false);
    }
}
