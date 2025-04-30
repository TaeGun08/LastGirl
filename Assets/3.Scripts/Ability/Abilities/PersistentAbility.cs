using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentAbility : Ability
{
    protected override IEnumerator CoolTimeCoroutine()
    {
        float timer = data.cooldown;

        while (gameObject.activeInHierarchy)
        {
            if (particle.gameObject.activeInHierarchy == false) continue;
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Collider[] colliders = Physics.OverlapSphere(localPlayer.transform.position, 
                    25f, LayerMask.GetMask("Enemy"));

                if (colliders.Length > 0)
                {
                    transform.position = colliders[0].transform.position;
                    transform.forward = localPlayer.transform.forward;
                }

                yield return new WaitForSeconds(0.1f);
                
                particle.Play();

                timer = data.cooldown;
            }

            yield return null;
        }
    }
    
    protected override void UsePersistentAbility(CombatEvent e)
    {
        StartCoroutine(nameof(CoolTimeCoroutine));
    }
}
