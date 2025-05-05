using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LobbyParts : MonoBehaviour
{
    protected AudioManager audioManager;
    
    [Header("Toga Animator")]
    [SerializeField] protected Animator anim;

    protected bool isInteractionEnabled;

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }
    
    protected abstract IEnumerator OnInteractionCoroutine();

    public void PartsInteraction()
    {
        if (isInteractionEnabled) return;
        StartCoroutine(nameof(OnInteractionCoroutine));
    }
}
