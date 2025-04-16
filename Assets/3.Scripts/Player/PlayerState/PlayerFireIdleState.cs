using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireIdleState : PlayerState
{
    private static readonly int FIRE = Animator.StringToHash("Fire");
    public override StateName Name => StateName.FireIdle;
    
    private Camera mainCam;
    
    private void Awake()
    {
        mainCam = Camera.main;
    }

    public override void StateEnter()
    {
        localPlayer.isShot = true;
        animator.SetTrigger(FIRE);
        animator.SetLayerWeight(1, 1f);
    }

    public override void StateUpdate(PlayerController playerController)
    {
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        Quaternion targetRotation = mainCam.transform.rotation;
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        localPlayer.transform.rotation = 
            Quaternion.Slerp(localPlayer.transform.rotation, targetRotation, Time.deltaTime * 10f);
        
        
        playerController.WeaponTrs.localRotation = Quaternion.Euler(0f, -mainCam.transform.eulerAngles.x, 0f);
        
        if (Input.GetMouseButton(0))
        {
            if (inputAxis.Equals(Vector2.zero) == false)
                playerController.ChangeState(StateName.FireWalk);
            return;
        }
        
        if (!localPlayer.IsZoom)
        {
            playerController.ChangeState(StateName.Idle);
            playerController.WeaponTrs.localRotation = Quaternion.identity;
            return;
        }
        
        if (inputAxis.Equals(Vector2.zero)) return;
        playerController.WeaponTrs.localRotation = Quaternion.identity;
        playerController.ChangeState(StateName.FireWalk);
    }

    public override void StateExit()
    {
        localPlayer.isShot = false;
        animator.SetLayerWeight(1, 0f);
        animator.ResetTrigger(FIRE);
        gameObject.SetActive(false);
    }
}
