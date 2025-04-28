using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsGameStart { get; set; }
    public bool IsGameOver { get; set; }

    private void Awake()
    {
        Instance = this;
    }
}
