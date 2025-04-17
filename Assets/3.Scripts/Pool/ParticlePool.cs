using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour, IEffectPool
{
    public IEffectPool.ParticleType Type => IEffectPool.ParticleType.HitA;
    
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        if (!particle.isPlaying)
        {
            particle.gameObject.SetActive(false);
        }
    }
    
}
