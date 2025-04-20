using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour, IEffectPool
{
    public IEffectPool.ParticleType Type => type;
    
    [Header("ParticlePool Settings")]
    [SerializeField] private IEffectPool.ParticleType type;
    
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    
    private void LateUpdate()
    {
        if (particle.isPlaying == false)
        {
            particle.gameObject.SetActive(false);
        }
    }
    
}
