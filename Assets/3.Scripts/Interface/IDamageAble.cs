using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrowdControlType
{
    Unknown,
    Stun,
    Slow,
}

public interface IDamageAble
{
    public HasParts HasParts { get; }
    public CrowdControlType CCType { get; set; }
    public GameObject GameObject { get; }
    
    public void TakeDamage(CombatEvent combatEvent);
}