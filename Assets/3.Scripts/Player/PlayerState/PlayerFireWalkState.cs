using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireWalkState : PlayerState
{
    private static readonly int FIRE = Animator.StringToHash("Fire");
    private static readonly int FIRE_IK = Animator.StringToHash("FireIK");
    private static readonly int FORWARD_MOVE = Animator.StringToHash("ForwardMove");
    private static readonly int LEFT_MOVE = Animator.StringToHash("LeftMove");

    public override StateName Name => StateName.FireWalk;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public override void StateEnter(PlayerController playerController)
    {
        this.playerController = playerController;
        localPlayer.IsShotReady = true;
        animator.SetLayerWeight(localPlayer.IsReload ? 2 : 1, 1f);
        animator.SetTrigger(FIRE);
    }

    public override void StateUpdate()
    {
        if (localPlayer.CCType.Equals(CrowdControlType.Unknown) == false)
        {
            playerController.ChangeState(StateName.CrowdControl);
            return;
        }
        
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
        
        playerController.ReloadWeapon(inputAxis);
        if (localPlayer.IsReload)
        {
            if (inputAxis.Equals(Vector2.zero)) 
                playerController.ChangeState(StateName.FireIdle);
            return;
        }
        
        playerController.WeaponTrs.localRotation = Quaternion.Euler(0f, -mainCam.transform.eulerAngles.x, 0f);
        
        if (Input.GetMouseButton(0))
        {
            if (inputAxis.Equals(Vector2.zero))
            {
                playerController.ChangeState(StateName.FireIdle);
                return;
            }
            
            if (playerController.currentWeapon.Data.Ammo <= 0f) return;
            if (!playerController.currentWeapon.Fire()) return;
            animator.ResetTrigger(FIRE_IK);
            animator.SetTrigger(FIRE_IK);
            
            return;
        }

        if (!localPlayer.IsZoom)
        {
            playerController.ChangeState(inputAxis.Equals(Vector2.zero) ? StateName.Idle : StateName.Walk);
            playerController.WeaponTrs.localRotation = Quaternion.identity;
            return;
        }

        if (inputAxis.Equals(Vector2.zero) == false && localPlayer.IsZoom) return;
        playerController.WeaponTrs.localRotation = Quaternion.identity;
        playerController.ChangeState(StateName.FireIdle);
    }

    public override void StateExit()
    {
        localPlayer.IsShotReady = false;
        animator.SetLayerWeight(1, 0f);
        animator.ResetTrigger(FIRE);
        gameObject.SetActive(false);
    }
}