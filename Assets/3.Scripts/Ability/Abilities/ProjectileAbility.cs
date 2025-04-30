using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : Ability
{
    protected override IEnumerator CoolTimeCoroutine()
    {
        AbilityOn = true;
        yield return new WaitForSeconds(0.1f);
        particle.Play();
        yield return new WaitForSeconds(data.cooldown);
        AbilityOn = false;
    }

    protected override void UseProjectileAbility(CombatEvent e)
    {
        if (AbilityOn || particle.gameObject.activeInHierarchy) return;
        
        transform.position = e.FirePoint.position;
        transform.forward = e.FirePoint.forward;
        
        StartCoroutine(nameof(CoolTimeCoroutine));
    }
}
