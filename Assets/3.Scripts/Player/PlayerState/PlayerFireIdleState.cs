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
        animator.SetTrigger(FIRE);
    }

    public override void StateUpdate(PlayerController playerController)
    {
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        Quaternion targetRotation = mainCam.transform.rotation;
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        localPlayer.transform.rotation = 
            Quaternion.Slerp(localPlayer.transform.rotation, targetRotation, Time.deltaTime * 10f);

        if (Input.GetMouseButton(0))
        {
            if (inputAxis.Equals(Vector2.zero) == false)
                playerController.ChangeState(StateName.FireWalk);
            
            return;
        }
        
        if (!localPlayer.IsZoom)
        {
            playerController.ChangeState(StateName.Idle);
            return;
        }
        
        if (inputAxis.Equals(Vector2.zero)) return;
        playerController.ChangeState(StateName.FireWalk);
    }

    public override void StateExit()
    {
        animator.ResetTrigger(FIRE);
        gameObject.SetActive(false);
    }
}
