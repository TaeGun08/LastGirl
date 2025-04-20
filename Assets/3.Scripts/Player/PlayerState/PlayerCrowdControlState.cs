using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrowdControlState : PlayerState
{
    private static readonly int CROWD_CONTROL = Animator.StringToHash("CrowdControl");
    public override StateName Name => StateName.CrowdControl;

    private float ccTimer;
    
    [Header("CC Effect")]
    [SerializeField] private ParticleSystem[] ccEffect;
    
    public override void StateEnter(PlayerController playerController)
    {
        this.playerController = playerController;
        animator.SetTrigger(CROWD_CONTROL);
        switch (localPlayer.CCType)
        {
            case CrowdControlType.Stun:
                ccEffect[0].gameObject.SetActive(true);
                break;
            case CrowdControlType.Slow:
                break;
            case CrowdControlType.Unknown:
                break;
        }
        ccTimer = 1f;
    }

    public override void StateUpdate()
    {
        ccTimer -= Time.deltaTime;
        if (ccTimer > 0) return;
        
        localPlayer.CCType = CrowdControlType.Unknown;
        playerController.ChangeState(StateName.Idle);
    }

    public override void StateExit()
    {
        ccEffect[0].gameObject.SetActive(false);
        animator.ResetTrigger(CROWD_CONTROL);
        gameObject.SetActive(false);
    }
}
