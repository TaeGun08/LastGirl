using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    protected override IEnumerator CoolTimeCoroutine()
    {
        AbilityOn = true;
        yield return new WaitForSeconds(0.5f);
        particle.gameObject.SetActive(true);
        particle.Play();
        yield return new WaitForSeconds(data.cooldown);
        AbilityOn = false;
    }

    protected override void UseDahsAbility(CombatEvent e)
    {
        if (AbilityOn || particle.gameObject.activeInHierarchy) return;

        particle.transform.position = e.FirePoint.position;
        particle.transform.forward = e.FirePoint.forward;

        StartCoroutine(nameof(CoolTimeCoroutine));
    }
}