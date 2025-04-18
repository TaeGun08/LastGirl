using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageAble
{
    [System.Serializable]
    public class EnemyData
    {
        public int Hp;
        public int MaxHp;
        public int Armor;
        public int Damage;
        public int Speed;
        public int AttackDelay;
        public int HasArca;
    }

    [Header("EnemyData Settings")] 
    [SerializeField] protected EnemyData Data;

    [Header("Enemy Settings")] 
    [SerializeField] protected Collider[] headHitColliders;
    [SerializeField] protected Collider[] bodyHitColliders;
    [SerializeField] protected Collider[] legHitColliders;
    [SerializeField] protected Collider[] armHitColliders;
    protected LocalPlayer localPlayer;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected bool isMoveStop;
    protected bool isDead;
    

    protected virtual void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Data.Speed;
        // foreach (Collider hitCollider in headHitColliders)
        //     CombatSystem.Instance.AddHitPartType(hitCollider, PartType.Head);
        //
        // foreach (Collider hitCollider in bodyHitColliders)
        //     CombatSystem.Instance.AddHitPartType(hitCollider, PartType.Body);
        //
        // foreach (Collider hitCollider in legHitColliders)
        //     CombatSystem.Instance.AddHitPartType(hitCollider, PartType.Leg);
        //
        // foreach (Collider hitCollider in armHitColliders)
        //     CombatSystem.Instance.AddHitPartType(hitCollider, PartType.Arm);
    }

    protected virtual void Update()
    {
        UpdateMovement();
        UpdatePattern();
    }

    protected abstract void UpdateMovement();

    protected abstract void UpdatePattern();

    public void TakeDamage(CombatEvent combatEvent)
    {
        if (isDead) return;
        
        //Data.Hp -= damage * (int)part;

        if (Data.Hp > 0f) return;
        isDead = true;
        Destroy(gameObject);
    }
}