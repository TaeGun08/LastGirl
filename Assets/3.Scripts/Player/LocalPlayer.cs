using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player, IDamageAble
{
    public PlayerStatus status;
    public Transform camLookAtPoint;

    public HasParts HasParts { get; }
    
    public CrowdControlType CCType { get; set; }
    
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

    public void TakeDamage(CombatEvent combatEvent)
    {
        if (IsDead) return;
        
        int damage = combatEvent.Damage;
        
        damage -= status.Durability;
        if (status.Durability > 0)
        {
            status.Durability -= combatEvent.Damage;
            if (status.Durability <= 0) status.Durability = 0;
        }
        else
        {
            status.Hp -= damage;
        }
        
        CCType = combatEvent.CCType;

        if (status.Hp > 0) return;
        IsDead = true;
        playerController.ChangeState(PlayerState.StateName.Dead);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}