using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    private static readonly int WALK = Animator.StringToHash("Walk");
    public override StateName Name => StateName.Walk;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public override void StateEnter()
    {
        animator.SetTrigger(WALK);
    }

    public override void StateUpdate(PlayerController playerController)
    {
        if (localPlayer.IsZoom)
        {
            playerController.ChangeState(StateName.FireIdle);
            return;
        }
        
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerController.CharacterController.Move(transform.forward.normalized *
                                                  (playerController.LocalPlayer.status.WalkSpeed * Time.deltaTime));

        if (Input.GetMouseButtonDown(0))
        {
            playerController.ChangeState(StateName.FireIdle);
            return;
        }
        
        if (inputAxis.x != 0)
        {
            UpdateRotation(inputAxis.x, mainCam.transform.right, Time.deltaTime * 5f);
        }
        
        if (inputAxis.y != 0)
        {
            UpdateRotation(inputAxis.y, mainCam.transform.forward, Time.deltaTime * 5f);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerController.ChangeState(StateName.Run);
            return;
        }

        if (!inputAxis.Equals(Vector2.zero)) return;

        playerController.ChangeState(StateName.Idle);
    }

    private void UpdateRotation(float inputAxis, Vector3 rotateDirection, float rotateSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(inputAxis > 0 ? 
            rotateDirection : -rotateDirection);
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        localPlayer.transform.rotation = Quaternion.Slerp(localPlayer.transform.rotation, targetRotation, rotateSpeed);
    }

    public override void StateExit()
    {
        animator.ResetTrigger(WALK);
        gameObject.SetActive(false);
    }
}