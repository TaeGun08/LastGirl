using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : Ability
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageAble hitAble = CombatSystem.Instance.GetHitAble(other);
        if (hitAble == null 
            || other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")) == false) return;
        CombatEvent e =  new CombatEvent();
        e.Sender = localPlayer;
        e.Receiver = hitAble;
        e.Damage = data.damage;
        e.Collider = other;
        e.HitPosition = other.transform.position;
        e.HasParts = hitAble.HasParts;
                
        CombatSystem.Instance.AddEvent(e);
    }
    
    protected override IEnumerator CoolTimeCoroutine()
    {
        AbilityOn = true;
        particle.Play();
        yield return new WaitForSeconds(data.endAbilityTime);
        particle.Stop();
        particle.Clear();
        yield return new WaitForSeconds(data.cooldown);
        AbilityOn = false;
    }

    protected override void UseProjectileAbility(CombatEvent e)
    {
        if (AbilityOn) return;
        
        transform.position = e.FirePoint.position;
        transform.forward = e.FirePoint.forward;
        
        StartCoroutine("CoolTimeCoroutine");
    }
}
