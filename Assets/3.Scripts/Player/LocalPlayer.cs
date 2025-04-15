using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player, IDamageAble
{
    public PlayerStatus status;
    public Transform camLookAtPoint;
    
    public bool IsZoom { get; set; }
    
    private void Awake()
    {
        LocalPlayer = this;
    }

    public void TakeDamage(int damage)
    {
    }
}