using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDashState : PlayerState
{
    private static readonly int RUN = Animator.StringToHash("Run");

    public override StateName Name => StateName.Dash;

    private Tween dashTween;
    
    [Header("DashEffect Settings")]
    [SerializeField] private ParticleSystem dashEffect;
    
    public override void StateEnter(PlayerController playerController)
    {
        this.playerController = playerController;
        animator.SetTrigger(RUN);

        localPlayer.IsDashing = true;
        localPlayer.UseDash = true;
        dashEffect.gameObject.SetActive(true);
        dashTween = this.playerController.transform.DOMove(transform.position + 
                                                           transform.forward * localPlayer.status.DashForce, 0.2f)
            .OnComplete(() =>
            {
                dashTween = null;
            });
    }

    public override void StateUpdate()
    { 
        if (dashTween != null) return;
        playerController.ChangeState(StateName.Run);
    }

    public override void StateExit()
    {
        localPlayer.UseDash = false;
        dashEffect.gameObject.SetActive(false);
        animator.ResetTrigger(RUN);
        gameObject.SetActive(false);
    }
}
