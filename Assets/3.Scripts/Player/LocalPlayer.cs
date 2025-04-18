using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player, IDamageAble
{
    public PlayerStatus status;
    public Transform camLookAtPoint;
    
    private void Awake()
    {
        LocalPlayer = this;
    }

    public void TakeDamage(CombatEvent combatEvent)
    {
        Debug.Log("아얏!");
    }
}