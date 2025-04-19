using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectPool
{
    public enum ParticleType
    {
        HitA,
        EnergyExplosionA,
    }

    public ParticleType Type { get; }
}
