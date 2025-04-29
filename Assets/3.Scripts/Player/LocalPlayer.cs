using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player, IDamageAble
{
    public PlayerStatus status;
    public Transform camLookAtPoint;

    public HasParts HasParts { get; }
    public GameObject GameObject => gameObject;
    
    public CrowdControlType CCType { get; set; }

    private float dashCoolTimer;

    public GameObject PlayerCanvas;
    
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
        GetArca();
        if (UseDash == false) return;
        
        dashCoolTimer -= Time.deltaTime;
        if (dashCoolTimer > 0) return;
        dashCoolTimer = status.DashCooldown;
        UseDash = false;
    }

    private void GetArca()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up, 10f,  
            LayerMask.GetMask("Arca"));

        if (colliders.Length <= 0) return;
        
        foreach (Collider collider in colliders)
        {
            Vector3 direction = (transform.position + Vector3.up - collider.transform.position).normalized;
            collider.transform.position += direction * (10f * Time.deltaTime);
            float distance = Vector3.Distance(new Vector3(collider.transform.position.x, 0f, collider.transform.position.z), 
                new Vector3(transform.position.x, 0f, transform.position.z));
            if ((distance <= 0.1f) == false) continue;
            status.HasArca++;
            collider.gameObject.SetActive(false);
        }
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