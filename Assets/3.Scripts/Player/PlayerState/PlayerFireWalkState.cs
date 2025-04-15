using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireWalkState : PlayerState
{
    private static readonly int FIRE = Animator.StringToHash("Fire");
    private static readonly int FORWARD_MOVE = Animator.StringToHash("ForwardMove");
    private static readonly int LEFT_MOVE = Animator.StringToHash("LeftMove");

    public override StateName Name => StateName.FireWalk;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public override void StateEnter()
    {
        animator.SetTrigger(FIRE);
    }

    public override void StateUpdate(PlayerController playerController)
    {
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveVec = localPlayer.transform.forward * inputAxis.y + localPlayer.transform.right * inputAxis.x;
        playerController.CharacterController.Move(moveVec *
                                                  (localPlayer.status.WalkSpeed / 2f * Time.deltaTime));

        Quaternion targetRotation = mainCam.transform.rotation;
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        localPlayer.transform.rotation =
            Quaternion.Slerp(localPlayer.transform.rotation, targetRotation, Time.deltaTime * 10f);

        animator.SetFloat(LEFT_MOVE, inputAxis.x);
        animator.SetFloat(FORWARD_MOVE, inputAxis.y);

        if (Input.GetMouseButton(0))
        {
            if (inputAxis.Equals(Vector2.zero))
                playerController.ChangeState(StateName.FireIdle);
            
            return;
        }

        if (!localPlayer.IsZoom)
        {
            playerController.ChangeState(StateName.Walk);
            return;
        }

        if (inputAxis.Equals(Vector2.zero) == false) return;
        playerController.ChangeState(!localPlayer.IsZoom ? StateName.Idle : StateName.FireIdle);
    }

    public override void StateExit()
    {
        animator.ResetTrigger(FIRE);
        animator.SetFloat(LEFT_MOVE, 0f);
        animator.SetFloat(FORWARD_MOVE, 0f);
        gameObject.SetActive(false);
    }
}