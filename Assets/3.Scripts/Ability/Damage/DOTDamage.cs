using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTDamage : MonoBehaviour
{
    private Ability ability;
    private ParticleSystem particle;    
    
    [Header("DOTDamage Settings")]
    [SerializeField] private BoxCollider attackCollider;
    [SerializeField] private float delay;
    [SerializeField] private float duration;
    private float delayTimer;
    private float durationTimer;

    private void OnEnable()
    {
        delayTimer = delay;
        durationTimer = duration;
    }

    private void Start()
    {
        ability = GetComponentInParent<Ability>();
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        delayTimer -= Time.deltaTime;
        if ((delayTimer <= 0) == false) return;
        Collider[] hitEnemy = Physics.OverlapBox(attackCollider.bounds.center, 
            attackCollider.bounds.size * 0.5f, Quaternion.identity, LayerMask.GetMask("Enemy"));

        if (hitEnemy.Length > 0)
        {
            foreach (Collider hit in hitEnemy)
            {
                IDamageAble hitAble = CombatSystem.Instance.GetHitAble(hit);
                if (hitAble == null) continue;
                CombatEvent e =  new CombatEvent();
                e.Receiver = hitAble;
                e.Damage = ability.Data.damage;
                e.Collider = hit;
                e.HitPosition = hit.transform.position;
                e.HasParts = hitAble.HasParts;
                
                CombatSystem.Instance.AddEvent(e);
            }
        }
            
        delayTimer = delay;
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
