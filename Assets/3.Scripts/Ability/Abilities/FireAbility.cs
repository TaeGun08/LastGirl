using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbility : Ability
{
    private IEnumerator CoolTimeCoroutine()
    {
        AbilityOn = true;
        particle.Play();
        yield return new WaitForSeconds(data.endAbilityTime);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(data.cooldown);
        AbilityOn = false;
    }
    
    public override void UseAbility(Transform target)
    {
        switch (type)
        {
            case AttackType.Area:
                transform.position = target.forward;
                break;
            case AttackType.Projectile:
                transform.position = target.forward;
                break;
            default:
                Debug.Log("보유한 능력이 없습니다");
                break;
        }
        
        StartCoroutine(nameof(CoolTimeCoroutine));
    }
}
