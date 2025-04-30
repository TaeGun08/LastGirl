using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierAbility : Ability
{
    protected override IEnumerator CoolTimeCoroutine()
    {
        float timer = data.cooldown;

        while (gameObject.activeInHierarchy)
        {
            if (localPlayer.IsBarrierOn == false)
            {
                if (particle.isPlaying)
                {
                    particle.Stop();
                    particle.Clear();
                }

                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    particle.Play();
                    timer = data.cooldown;
                    localPlayer.IsBarrierOn = true;
                }
            }

            yield return null;
        }
    }

    protected override void Start()
    {
        base.Start();
        CombatEvent e = new CombatEvent();
        e.FirePoint = localPlayer.transform;
        AbilitySystem.Instance.Events.OnBarrierAbilityEvent?.Invoke(e);
    }

    protected override void UseBarrierAbility(CombatEvent e)
    {
        transform.position = e.FirePoint.position + Vector3.up * 0.8f;
        transform.forward = e.FirePoint.forward;
        transform.SetParent(localPlayer.transform);
        particle.Play();
        localPlayer.IsBarrierOn = true;

        StartCoroutine("CoolTimeCoroutine");
    }
}