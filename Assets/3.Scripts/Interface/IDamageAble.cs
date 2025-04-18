using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    Unknown,
    Arm,
    Leg,
    Body,
    Head,
}

public interface IDamageAble
{
    public void TakeDamage(CombatEvent combatEvent);
}
