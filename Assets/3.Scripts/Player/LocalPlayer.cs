using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player, IDamageAble
{
    public PlayerStatus status;
    public Transform camLookAtPoint;

    private void Awake()
    {
        LocalPlayer = this;
    }

    private void Start()
    {
        foreach (Collider hitCollider in Parts.HeadHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);

        foreach (Collider hitCollider in Parts.BodyHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);

        foreach (Collider hitCollider in Parts.LegHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);

        foreach (Collider hitCollider in Parts.ArmHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);
    }

    public HasParts HasParts { get; }

    public void TakeDamage(CombatEvent combatEvent)
    {
        if (IsDead) return;
        
        status.Hp -= combatEvent.Damage;
        
        if (status.Hp <= 0)
        {
            IsDead = true;
        }
    }
}