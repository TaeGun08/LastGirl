using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAbility : Ability
{
    protected override IEnumerator CoolTimeCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        float timer = data.cooldown;

        while (gameObject.activeInHierarchy)
        {
            if (particle.gameObject.activeInHierarchy == false) continue;
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                transform.position = localPlayer.transform.position;
                transform.forward = localPlayer.transform.forward;

                yield return new WaitForSeconds(0.1f);
                
                particle.Play();
                particle.gameObject.SetActive(true);
                
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
        StartCoroutine(nameof(CoolTimeCoroutine));
    }
}
