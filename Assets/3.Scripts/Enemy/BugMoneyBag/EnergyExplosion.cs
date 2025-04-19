using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyExplosion : MonoBehaviour
{
    public int Damage { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;
        IDamageAble player = other.GetComponent<IDamageAble>();
        if (player == null) return;
        CombatEvent e = new CombatEvent();
        e.Damage = Damage;
        e.Receiver = player;
        CombatSystem.Instance.AddEvent(e);
    }
}
