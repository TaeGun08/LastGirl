using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gravity : MonoBehaviour
{
    [Header("Gravity Settings")]
    [SerializeField] protected float gravity;

    protected float velocity;
    
    public abstract void UpdateGravity();
}
