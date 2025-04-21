using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")) == false) return;
        IDamageAble player = other.GetComponent<IDamageAble>();
        if (player == null) return;
        CombatEvent e = new CombatEvent();
        e.Damage = enemy.Data.Damage;
        e.Receiver = player;
        e.CCType = CrowdControlType.Stun;
        
        CombatSystem.Instance.AddEvent(e);
        
        gameObject.SetActive(false);
    }
}
