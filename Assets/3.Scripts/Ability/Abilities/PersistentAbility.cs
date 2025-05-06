using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PersistentAbility : Ability
{
    [FormerlySerializedAs("playerForward")] [SerializeField] private bool playerForwardOff;
    
    protected override IEnumerator CoolTimeCoroutine()
    {
        float timer = data.cooldown;

        while (gameObject.activeInHierarchy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Collider[] colliders = Physics.OverlapSphere(localPlayer.transform.position, 
                    25f, LayerMask.GetMask("Enemy"));

                if (colliders.Length > 0)
                {
                    Vector3 spawnPoint = colliders[0].transform.position;
                    spawnPoint.y = localPlayer.transform.position.y;
                    particle.transform.position = spawnPoint;
                    if (playerForwardOff == false)
                    {
                        particle.transform.forward = localPlayer.transform.forward;
                    }
                }

                yield return new WaitForSeconds(0.1f);
                
                particle.gameObject.SetActive(true);
                particle.Play();

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
        AbilitySystem.Instance.Events.OnPersistentAbilityEvent?.Invoke(e);
    }
    
    protected override void UsePersistentAbility(CombatEvent e)
    {
        StartCoroutine(nameof(CoolTimeCoroutine));
    }
}
