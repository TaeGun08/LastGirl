using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbility : Ability
{
    [SerializeField] private bool setPlayerTrs;
    
    protected override IEnumerator CoolTimeCoroutine()
    {
        AbilityOn = true;
        yield return new WaitForSeconds(0.1f);
        particle.gameObject.SetActive(true);
        particle.Play();
        yield return new WaitForSeconds(data.cooldown);
        AbilityOn = false;
    }
    
    protected override void UseFireAbility(CombatEvent e)
    {
        if (AbilityOn || particle.gameObject.activeInHierarchy) return;
        
        particle.transform.position = e.FirePoint.position;
        particle.transform.forward = e.FirePoint.forward;
        if (setPlayerTrs)
        {
            particle.transform.SetParent(localPlayer.transform);
        }
        
        StartCoroutine(nameof(CoolTimeCoroutine));
    }
}
