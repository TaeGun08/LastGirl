using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public enum AttackType
    {
        Area,
        Projectile,
    }

    [System.Serializable]
    public class AbilityData
    {
        public float damage;
        public float radius;
        public float endAbilityTime;
        public float cooldown;
        public float speed;
        public float attackDelay;
    }

    [Header("Ability Settings")]
    [SerializeField] protected AbilityData data;
    public bool AbilityOn;
    [SerializeField] protected ParticleSystem particle;
    
    [Header("AttackType Settings")]
    [SerializeField] protected AttackType type;
    public AttackType Type => type;

    public abstract void UseAbility(Transform target);
}
