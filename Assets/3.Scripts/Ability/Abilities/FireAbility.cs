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
        Collider[] hitEnemy = Physics.OverlapBox(attackCollider.bounds.center, attackCollider.bounds.size,
            Quaternion.identity, LayerMask.GetMask("Enemy"));

        if (hitEnemy.Length > 0)
        {
            foreach (Collider hit in hitEnemy)
            {
                IDamageAble hitAble = CombatSystem.Instance.GetHitAble(hit);
                if (hitAble == null) continue;
                CombatEvent e =  new CombatEvent();
                e.Sender = localPlayer;
                e.Receiver = hitAble;
                e.Damage = data.damage;
                e.Collider = hit;
                e.HitPosition = hit.transform.position;
                e.HasParts = hitAble.HasParts;
                
                CombatSystem.Instance.AddEvent(e);
            }
        }
        yield return new WaitForSeconds(data.endAbilityTime);
        particle.Stop();
        particle.Clear();
        yield return new WaitForSeconds(data.cooldown);
        AbilityOn = false;
    }
    
    protected override void UseFireAbility(CombatEvent e)
    {
        if (AbilityOn) return;
        
        Debug.Log("발사능력발동");
        transform.position = e.FirePoint.position;
        transform.forward = e.FirePoint.forward;
        
        StartCoroutine("CoolTimeCoroutine");
    }
}
