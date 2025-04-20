using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private static readonly int IDLE = Animator.StringToHash("Idle");

    public override StateName Name => StateName.Idle;

    public override void StateEnter(PlayerController playerController)
    {
        this.playerController = playerController;
        animator.SetTrigger(IDLE);
    }

    public override void StateUpdate()
    {
        if (localPlayer.CCType.Equals(CrowdControlType.Unknown) == false)
        {
            playerController.ChangeState(StateName.CrowdControl);
            return;
        }
        
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        playerController.ReloadWeapon(inputAxis);
        
        if(localPlayer.IsReload) return;
        
        if (localPlayer.IsZoom)
        {
            playerController.ChangeState(StateName.FireIdle);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            playerController.ChangeState(StateName.FireIdle);
            return;
        }
        
        if (inputAxis.Equals(Vector2.zero)) return;
        playerController.ChangeState(Input.GetKeyDown(KeyCode.LeftShift) ? StateName.Run : StateName.Walk);
    }

    public override void StateExit()
    {
        animator.ResetTrigger(IDLE);
        gameObject.SetActive(false);
    }
}