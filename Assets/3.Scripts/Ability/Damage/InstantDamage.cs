using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamage : MonoBehaviour
{
    private Ability ability;
    private ParticleSystem particle;       
    
    [Header("InstantDamage Settings")]
    [SerializeField] private float duration;
    private float durationTimer;
    
    private void OnEnable()
    {
        durationTimer = duration;
    }
    
    private void Start()
    {
        ability = GetComponentInParent<Ability>();
        particle = GetComponent<ParticleSystem>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IDamageAble hitAble = CombatSystem.Instance.GetHitAble(other);
        if (hitAble == null 
            || other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")) == false) return;
        CombatEvent e =  new CombatEvent();
        e.Receiver = hitAble;
        e.Damage = ability.Data.damage;
        e.Collider = other;
        e.HitPosition = other.transform.position;
        e.HasParts = hitAble.HasParts;
                
        CombatSystem.Instance.AddEvent(e);
    }

    private void LateUpdate()
    {
        durationTimer -= Time.deltaTime;

        if ((durationTimer <= 0f) == false) return;
        durationTimer = duration;
        particle.Stop();
        particle.Clear();
        gameObject.SetActive(false);
    }
}
