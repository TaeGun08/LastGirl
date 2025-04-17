using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EffectPoolSystem : MonoBehaviour
{
    public static EffectPoolSystem Instance;
    
    [Header("Effects")]
    [SerializeField] private ParticlePool[] particles;
    private Dictionary<IEffectPool.ParticleType, ParticlePool> effectPoolDic 
        =  new Dictionary<IEffectPool.ParticleType, ParticlePool>();
    
    private Queue<ParticlePool> effectPoolQueue = new Queue<ParticlePool>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            effectPoolDic.Add(particles[i].Type, particles[i]);
        }
    }

    public ParticlePool ParticlePool(IEffectPool.ParticleType type)
    {
        ParticlePool particle = null;

        if (effectPoolQueue.Count > 0)
        {
            for (int i = 0; i < effectPoolQueue.Count; i++)
            {
                particle = effectPoolQueue.Dequeue();
                if (particle.gameObject.activeInHierarchy == false)
                {
                    effectPoolQueue.Enqueue(particle);
                    particle.gameObject.SetActive(true);
                    return particle;
                } 
                effectPoolQueue.Enqueue(particle);
            }  
        }
        
        particle = Instantiate(effectPoolDic[type], transform);
        effectPoolQueue.Enqueue(particle);
        return particle;
    }
}
