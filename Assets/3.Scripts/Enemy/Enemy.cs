using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : HasParts, IDamageAble
{
    private static readonly int DEAD = Animator.StringToHash("Dead");
    private static readonly int HIT = Animator.StringToHash("Hit");

    [System.Serializable]
    public class EnemyData
    {
        public int Hp;
        public int MaxHp;
        public int Armor;
        public int Damage;
        public float Speed;
        public float AttackDelay;
        public int HasArca;
    }

    [Header("EnemyData Settings")] 
    [SerializeField] protected EnemyData Data;

    public HasParts HasParts => this;
    
    protected LocalPlayer localPlayer;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected bool isMoveStop;
    protected bool isDead;
    [Header("EnemyDeadTime Settings")]
    [SerializeField] protected float deathTime;

    protected virtual void Start()
    {
        base.Start();
        
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        
        foreach (Collider hitCollider in Parts.HeadHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);
        
        foreach (Collider hitCollider in Parts.BodyHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);
        
        foreach (Collider hitCollider in Parts.LegHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);
        
        foreach (Collider hitCollider in Parts.ArmHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);
        
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Data.Speed;
    }

    protected virtual void Update()
    {
        if (isDead)
        {
            agent.SetDestination(transform.position);
            return;
        };
        
        UpdateMovement();
        UpdatePattern();
    }

    protected abstract void UpdateMovement();

    protected abstract void UpdatePattern();

    public void TakeDamage(CombatEvent combatEvent)
    {
        if (isDead) return;
        
        animator.ResetTrigger(HIT);
        animator.SetTrigger(HIT);
        Data.Hp -= (combatEvent.Damage * 
                    (int)combatEvent.HasParts.GetPartsType(combatEvent.Collider)) - Data.Armor;

        if (Data.Hp > 0f) return;
        StartCoroutine(OnDeadCoroutine(deathTime));
    }

    private IEnumerator OnDeadCoroutine(float deathTime)
    {
        isDead = true;
        animator.SetTrigger(DEAD);
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}