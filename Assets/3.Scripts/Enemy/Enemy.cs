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

    [Header("EnemyData Settings")] [SerializeField]
    protected EnemyData Data;

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
    }

    protected virtual void Update()
    {
        UpdateMovement();
        UpdatePattern();
    }

    protected abstract void UpdateMovement();

    protected abstract void UpdatePattern();

    public void TakeDamage(int damage, PartType part)
    {
        if (isDead) return;
        
        Data.Hp -= damage * (int)part;

        if (Data.Hp <= 0f)
        {
            isDead = true;
        }
    }
}