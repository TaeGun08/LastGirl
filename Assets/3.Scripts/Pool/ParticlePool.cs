using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour, IEffectPool
{
    public IEffectPool.ParticleType Type => type;
    
    [Header("ParticlePool Settings")]
    [SerializeField] private IEffectPool.ParticleType type;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private bool activeOn;
    
    private void LateUpdate()
    {
        if (particle.isPlaying == false && activeOn == false)
        {
            particle.gameObject.SetActive(false);
        }
    }
    
}
