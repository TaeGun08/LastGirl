using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    private static readonly int RUN = Animator.StringToHash("Run");
    public override StateName Name => StateName.Run;

    public override void StateEnter()
    {
        animator.SetTrigger(RUN);
    }

    public override void StateUpdate(PlayerController playerController)
    {
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveVec = new Vector3(inputAxis.x, 0, inputAxis.y);
        playerController.CharacterController.Move(moveVec * 
                                                  (playerController.LocalPlayer.status.RunSpeed * Time.deltaTime));

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            playerController.ChangeState(StateName.Walk);
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
