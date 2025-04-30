using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    protected override IEnumerator CoolTimeCoroutine()
    {
        AbilityOn = true;
        yield return new WaitForSeconds(0.5f);
        particle.Play();
        yield return new WaitForSeconds(data.cooldown);
        AbilityOn = false;
    }

    protected override void UseDahsAbility(CombatEvent e)
    {
        if (AbilityOn) return;
        
        transform.position = e.FirePoint.position;
        transform.forward = e.FirePoint.forward;
        
        StartCoroutine("CoolTimeCoroutine");
    }
}
