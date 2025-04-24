using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireIdleState : PlayerState
{
    private static readonly int FIRE = Animator.StringToHash("Fire");
    private static readonly int FIRE_IK = Animator.StringToHash("FireIK");
    private static readonly int FORWARD_MOVE = Animator.StringToHash("ForwardMove");
    private static readonly int LEFT_MOVE = Animator.StringToHash("LeftMove");
    public override StateName Name => StateName.FireIdle;

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
        animator.SetFloat(LEFT_MOVE, 0f);
        animator.SetFloat(FORWARD_MOVE, 0f);
    }

    public override void StateUpdate()
    {
        if (localPlayer.CCType.Equals(CrowdControlType.Unknown) == false)
        {
            playerController.ChangeState(StateName.CrowdControl);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && localPlayer.UseDash == false)
        {
            playerController.ChangeState(StateName.Dash);
            return;
        }

        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Quaternion targetRotation = mainCam.transform.rotation;
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        localPlayer.transform.rotation =
            Quaternion.Slerp(localPlayer.transform.rotation, targetRotation, Time.deltaTime * 10f);


        playerController.ReloadWeapon(inputAxis);
        if (localPlayer.IsReload)
        {
            if (inputAxis.Equals(Vector2.zero) == false)
                playerController.ChangeState(StateName.FireWalk);
            return;
        }
        
        int layer = LayerMask.GetMask("Enemy", "Map");
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200f, layer))
        {
            localPlayer.aimPoint.position = hit.point;
        }
        
        playerController.WeaponTrs.localRotation = Quaternion.Euler(0f, -mainCam.transform.eulerAngles.x, 0f);

        if (Input.GetMouseButton(0))
        {
            if (inputAxis.Equals(Vector2.zero) == false)
            {
                playerController.ChangeState(StateName.FireWalk);
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
            playerController.ChangeState(StateName.Idle);
            playerController.WeaponTrs.localRotation = Quaternion.identity;
            return;
        }

        if (inputAxis.Equals(Vector2.zero) && localPlayer.IsZoom) return;
        playerController.WeaponTrs.localRotation = Quaternion.identity;
        playerController.ChangeState(StateName.FireWalk);
    }

    public override void StateExit()
    {
        localPlayer.IsShotReady = false;
        animator.SetLayerWeight(1, 0f);
        animator.ResetTrigger(FIRE);
        gameObject.SetActive(false);
    }
}