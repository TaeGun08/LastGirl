using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbility : Ability
{
    protected override IEnumerator CoolTimeCoroutine()
    {
        AbilityOn = true;
        particle.Play();
        particle.gameObject.SetActive(true);
        yield return new WaitForSeconds(data.cooldown);
        AbilityOn = false;
    }
    
    protected override void UseFireAbility(CombatEvent e)
    {
        if (AbilityOn || particle.gameObject.activeInHierarchy) return;
        
        transform.position = e.FirePoint.position;
        transform.forward = e.FirePoint.forward;
        
        StartCoroutine(nameof(CoolTimeCoroutine));
    }
}
