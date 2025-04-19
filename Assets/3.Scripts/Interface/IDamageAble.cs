using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble
{
    public HasParts HasParts { get;}
    
    public void TakeDamage(CombatEvent combatEvent);
}
