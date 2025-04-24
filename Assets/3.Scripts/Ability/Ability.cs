using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [System.Serializable]
    public class AbilityData
    {
        public int key;
        public int damage;
        public float endAbilityTime;
        public float cooldown;
        public float attackDelay;
    }
    
    protected LocalPlayer localPlayer;

    [Header("Ability Settings")]
    [SerializeField] protected AbilityData data;
    public AbilityData Data => data;
    public bool AbilityOn;
    [SerializeField] protected ParticleSystem particle;
    [SerializeField] protected Collider attackCollider;

    protected virtual void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        AbilitySystem.Instance.Events.OnFireAbilityEvent += UseFireAbility;
        AbilitySystem.Instance.Events.OnDashAbilityEvent += UseDahsAbility;
        AbilitySystem.Instance.Events.OnBarrierAbilityEvent += UseBarrierAbility;
        AbilitySystem.Instance.Events.OnPersistentAbilityEvent += UsePersistentAbility;
        AbilitySystem.Instance.Events.OnAutoAbilityEvent += UseAutoAbility;
        AbilitySystem.Instance.Events.OnProjectileAbilityEvent += UseProjectileAbility;
    }

    protected void OnDestroy()
    {
        AbilitySystem.Instance.Events.OnFireAbilityEvent -= UseFireAbility;
        AbilitySystem.Instance.Events.OnDashAbilityEvent -= UseDahsAbility;
        AbilitySystem.Instance.Events.OnBarrierAbilityEvent -= UseBarrierAbility;
        AbilitySystem.Instance.Events.OnPersistentAbilityEvent -= UsePersistentAbility;
        AbilitySystem.Instance.Events.OnAutoAbilityEvent -= UseAutoAbility;
        AbilitySystem.Instance.Events.OnProjectileAbilityEvent -= UseProjectileAbility;

    }

    protected abstract IEnumerator CoolTimeCoroutine();

    protected virtual void UseFireAbility(CombatEvent e)
    {
    }

    protected virtual void UseDahsAbility(CombatEvent e)
    {
    }

    protected virtual void UseBarrierAbility(CombatEvent e)
    {
    }

    protected virtual void UsePersistentAbility(CombatEvent e)
    {
    }

    protected virtual void UseAutoAbility(CombatEvent e)
    {
    }

    protected virtual void UseProjectileAbility(CombatEvent e)
    {
    }
}
