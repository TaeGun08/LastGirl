using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerRunState : PlayerState
{
    private static readonly int RUN = Animator.StringToHash("Run");
    public override StateName Name => StateName.Run;

    private Camera mainCam;
    
    private void Awake()
    {
        mainCam = Camera.main;
    }

    public override void StateEnter()
    {
        animator.SetTrigger(RUN);
    }

    public override void StateUpdate(PlayerController playerController)
    {
        if (localPlayer.IsZoom)
        {
            playerController.ChangeState(StateName.FireIdle);
            return;
        }
        
        Vector2 inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerController.CharacterController.Move(transform.forward.normalized * 
                                                  (playerController.LocalPlayer.status.RunSpeed * Time.deltaTime));
        if (Input.GetMouseButtonDown(0))
        {
            playerController.ChangeState(StateName.FireIdle);
            return;
        }
        
        if (inputAxis != Vector2.zero)
        {
            UpdateRotation(inputAxis, Time.deltaTime * 5f);
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            playerController.ChangeState(StateName.Walk);
            return;
        }
        
        if (!inputAxis.Equals(Vector2.zero)) return;
        
        playerController.ChangeState(StateName.Idle);
    }
    
    private void UpdateRotation(Vector2 inputAxis, float rotateSpeed)
    {
        Vector3 moveVec = (mainCam.transform.forward * inputAxis.y + mainCam.transform.right *  inputAxis.x).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(moveVec);
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        localPlayer.transform.rotation = Quaternion.Slerp(localPlayer.transform.rotation, targetRotation, rotateSpeed);
    }

    public override void StateExit()
    {
        animator.ResetTrigger(RUN);
        gameObject.SetActive(false);
    }
}
