using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAbility : Ability
{
    protected override IEnumerator CoolTimeCoroutine()
    {
        float timer = data.cooldown;

        while (gameObject.activeInHierarchy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("자동능력발동");

                transform.position = localPlayer.transform.position;
                transform.forward = localPlayer.transform.forward;
                
                particle.Clear();
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
                
                timer = data.cooldown;
            }

            yield return null;
        }
    }

    protected override void Start()
    {
        base.Start();
        CombatEvent e = new CombatEvent();
        e.FirePoint = localPlayer.transform;
        AbilitySystem.Instance.Events.OnAutoAbilityEvent?.Invoke(e);
    }
    
    protected override void UseAutoAbility(CombatEvent e)
    {
        StartCoroutine("CoolTimeCoroutine");
    }
}
