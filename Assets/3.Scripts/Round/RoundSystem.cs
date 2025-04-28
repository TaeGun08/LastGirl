using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    public static RoundSystem Instance;
    
    [Header("RoundSystem Settings")]
    [SerializeField] private RoundCanvas roundCanvas;
    public RoundCanvas RoundCanvas => roundCanvas;
    public bool IsRoundStart { get; set; }
    public int SpawnEnemyCount { get; set; }

    private void Awake()
    {
        Instance = this;
    }
}
