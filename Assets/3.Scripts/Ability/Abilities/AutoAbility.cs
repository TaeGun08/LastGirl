using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAbility : Ability
{
    [SerializeField] private bool setPlayerTrs;
    [SerializeField] private bool playerForwardOff;
    
    protected override IEnumerator CoolTimeCoroutine()
    {
        float timer = data.cooldown;

        while (gameObject.activeInHierarchy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                particle.transform.position = localPlayer.transform.position;
                if (playerForwardOff == false)
                {
                    particle.transform.forward = localPlayer.transform.forward;
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
        if (setPlayerTrs)
        {
            transform.SetParent(localPlayer.transform);
        }
        AbilitySystem.Instance.Events.OnAutoAbilityEvent?.Invoke(e);
    }
    
    protected override void UseAutoAbility(CombatEvent e)
    {
        StartCoroutine(nameof(CoolTimeCoroutine));
    }
}
