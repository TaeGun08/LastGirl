using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private static readonly int IDLE = Animator.StringToHash("Idle");

    public override StateName Name => StateName.Idle;

    public override void StateEnter()
    {
        animator.SetTrigger(IDLE);
    }

    public override void StateUpdate(PlayerController playerController)
    {
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (inputAxis == Vector2.zero) return;
        playerController.ChangeState(Input.GetKeyDown(KeyCode.LeftShift) ? StateName.Run : StateName.Walk);
        StateExit();
    }

    public override void StateExit()
    {
        gameObject.SetActive(false);
    }
}