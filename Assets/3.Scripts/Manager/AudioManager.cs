using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("AudioSource Settings")]
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    [Header("Audio Settings")] 
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip sfxClip;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.Play();
    }

    public void SetBgmClip(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    public void SetSfxClip(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }
}
