using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectPool
{
    public enum ParticleType
    {
        HitA,
        EnergyExplosionA,
        Arca,
    }

    public ParticleType Type { get; }
}
