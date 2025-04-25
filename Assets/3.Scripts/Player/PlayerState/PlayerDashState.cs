using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDashState : PlayerState
{
    private static readonly int RUN = Animator.StringToHash("Run");

    public override StateName Name => StateName.Dash;

    private Tween dashTween;
    private bool isEnterDashing = false;
    
    [Header("DashEffect Settings")]
    [SerializeField] private ParticleSystem dashEffect;
    
    public override void StateEnter(PlayerController playerController)
    {
        this.playerController = playerController;
        animator.SetTrigger(RUN);

        localPlayer.IsDashing = true;
        localPlayer.UseDash = true;
        dashEffect.gameObject.SetActive(true);
        CombatEvent e =  new CombatEvent();
        e.FirePoint = localPlayer.transform;
        AbilitySystem.Instance.Events.OnDashAbilityEvent?.Invoke(e);
        StartCoroutine(nameof(DashCoroutine));
    }

    public override void StateUpdate()
    { 
        if (isEnterDashing) return;
        playerController.ChangeState(StateName.Run);
    }

    private IEnumerator DashCoroutine()
    {
        float timer = 0f;
        isEnterDashing = true;
        while (timer < 0.2f)
        {
            timer += Time.deltaTime;
            playerController.CharacterController.Move(transform.forward * localPlayer.status.DashForce * Time.deltaTime);
            yield return null;
        }
        isEnterDashing = false;
    }

    public override void StateExit()
    {
        localPlayer.IsDashing = false;
        dashEffect.gameObject.SetActive(false);
        animator.ResetTrigger(RUN);
        gameObject.SetActive(false);
    }
}
