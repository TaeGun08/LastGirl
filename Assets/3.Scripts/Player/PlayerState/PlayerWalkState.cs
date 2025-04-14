using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    private static readonly int WALK = Animator.StringToHash("Walk");

    public override StateName Name => StateName.Walk;

    public override void StateEnter()
    {
        animator.SetTrigger(WALK);
    }

    public override void StateUpdate(PlayerController playerController)
    {
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveVec = new Vector3(inputAxis.x, 0, inputAxis.y);
        playerController.CharacterController.Move(moveVec * 
                                                  (playerController.LocalPlayer.status.WalkSpeed * Time.deltaTime));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerController.ChangeState(StateName.Run);
            StateExit();
            return;
        }
        
        if (!inputAxis.Equals(Vector2.zero)) return;
        
        playerController.ChangeState(StateName.Idle);
        StateExit();
    }

    public override void StateExit()
    {
        gameObject.SetActive(false);
    }
}
