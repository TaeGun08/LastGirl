using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectPool
{
    public enum ParticleType
    {
        HitA,
        HitB,
        EnergyExplosionA,
        Arca,
    }

    public ParticleType Type { get; }
}
