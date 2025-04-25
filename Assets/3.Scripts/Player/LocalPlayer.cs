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

    private float dashCoolTimer;
    
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
        
        dashCoolTimer = status.DashCooldown;
    }

    private void Update()
    {
        if (UseDash == false) return;
        
        dashCoolTimer -= Time.deltaTime;
        if (dashCoolTimer > 0) return;
        dashCoolTimer = status.DashCooldown;
        UseDash = false;
    }

    public void TakeDamage(CombatEvent combatEvent)
    {
        if (IsBarrierOn)
        {
            IsBarrierOn = false;
            return;
        }
        
        if (IsDead || IsDashing) return;
        
        int damage = combatEvent.Damage;
        
        damage -= status.Durability;
        if (status.Durability > 0)
        {
            status.Durability -= combatEvent.Damage;
            if (status.Durability <= 0) status.Durability = 0;
        }
        
        if (status.Durability <= 0)
        {
            status.Hp -= damage;
        }
        
        CCType = combatEvent.CCType;

        if (status.Hp > 0) return;
        status.Hp = 0;
        IsDead = true;
        playerController.ChangeState(PlayerState.StateName.Dead);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}