using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReLoadState : PlayerState
{
    private static readonly int RELOAD = Animator.StringToHash("Reload");
    public override StateName Name => StateName.ReLoad;

    public override void StateEnter(PlayerController playerController)
    {
        this.playerController = playerController;
        animator.SetTrigger(RELOAD);
        animator.SetLayerWeight(1, 1f);
    }

    public override void StateUpdate()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if (stateInfo.IsName("ReLoad")) return;
        
        playerController.ChangeState(StateName.Idle);
    }

    public override void StateExit()
    {
        animator.ResetTrigger(RELOAD);
        animator.SetLayerWeight(1, 0f);
        gameObject.SetActive(false);
    }
}
