using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    Arm,
    Leg,
    Body,
    Head,
}

public interface IDamageAble
{
    public void TakeDamage(int damage, PartType part);
}
