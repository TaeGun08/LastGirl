using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class Enemy : HasParts, IDamageAble
{
    private static readonly int DEAD = Animator.StringToHash("Dead");
    private static readonly int HIT = Animator.StringToHash("Hit");
    protected static readonly int ATTACK = Animator.StringToHash("Attack");
    protected static readonly int PATTERN = Animator.StringToHash("Pattern");

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
    public EnemyData Data;

    public HasParts HasParts => this;
    
    [Header("EnemyCCType")]
    [SerializeField] protected CrowdControlType ccType;
    public CrowdControlType CCType { get; set; }

    [Header("Enemy Settings")]
    public GameObject[] attackColliders;
    public float chaseAngle;
    public int hasPattern;
    
    protected LocalPlayer localPlayer;
    public LocalPlayer LocalPlayer => localPlayer;
    
    protected Animator animator;
    public Animator Animator => animator;
    
    protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    
    [SerializeField] protected EnemyState[] states;
    protected EnemyState currentState;
    
    private Dictionary<EnemyState.StateName, EnemyState> enemyStateDic = new Dictionary<EnemyState.StateName, EnemyState>();
    
    protected bool isMoveStop;
    public bool isPattern;
    protected bool isDead;
    public float attackDelay;

    public int patternNumber;
    
    [Header("EnemyDeadTime Settings")]
    [SerializeField] protected float deathTime;

    protected virtual void Start()
    {
        base.Start();
        
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();

        CCType = ccType;

        attackDelay = Data.AttackDelay;
        
        foreach (Collider hitCollider in Parts.HeadHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);
        
        foreach (Collider hitCollider in Parts.BodyHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);
        
        foreach (Collider hitCollider in Parts.LegHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);
        
        foreach (Collider hitCollider in Parts.ArmHitColliders)
            CombatSystem.Instance.AddHitAbleType(hitCollider, this);
        
        for (int i = 0; i < states.Length; i++)
        {
            enemyStateDic.Add(states[i].Name, states[i]);
            states[i].gameObject.SetActive(false);
        }
        
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Data.Speed;
        
        if (states.Length <= 0) return;
        currentState = states[0];
        currentState.StateEnter(this);
    }

    protected virtual void Update()
    {
        if (isDead || isMoveStop)
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

        if (isPattern == false)
        {
            animator.ResetTrigger(HIT);
            animator.SetTrigger(HIT);
        }
        
        int damage = (combatEvent.Damage * 
                      (int)combatEvent.HasParts.GetPartsType(combatEvent.Collider)) - Data.Armor;

        if (damage <= 0)
        {
            Data.Hp -= 1;
        }
        else
        {
            Data.Hp -= damage;
        }

        if (Data.Hp > 0f) return;
        StartCoroutine(OnDeadCoroutine(deathTime));
    }

    private IEnumerator OnDeadCoroutine(float deathTime)
    {
        for (int i = 0; i < Data.HasArca; i++)
        {
            Arca arca = EffectPoolSystem.Instance.ParticlePool(IEffectPool.ParticleType.HitA).GetComponent<Arca>();
            arca.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),Random.Range(-1f, 1f)));
        }
        animator.ResetTrigger(HIT);
        isDead = true;
        animator.SetTrigger(DEAD);
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
    
    public void ChangeState(EnemyState.StateName name)
    {
        currentState.StateExit();
        currentState = enemyStateDic[name];
        currentState.gameObject.SetActive(true);
        currentState.StateEnter(this);
    }
}