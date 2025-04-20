using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyExplosion : MonoBehaviour
{
    public int Damage { get; set; }

    private bool isHit;

    private void OnEnable()
    {
        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")) == false || isHit) return;
        IDamageAble player = other.GetComponent<IDamageAble>();
        if (player == null) return;
        CombatEvent e = new CombatEvent();
        e.Damage = Damage;
        e.Receiver = player;
        e.CCType = CrowdControlType.Stun;
        
        CombatSystem.Instance.AddEvent(e);
        isHit = true;
    }
}
