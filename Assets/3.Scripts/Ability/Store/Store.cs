using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Store : MonoBehaviour
{
    protected AbilitySystem abilitySystem;
    protected RectTransform rectTrs;
    
    [Header("Store Settings")] 
    [SerializeField] protected AbilityStore abilityStore;
    [SerializeField] protected AbilityUI abilityUI;
    
    protected virtual void Start()
    {
        abilitySystem = AbilitySystem.Instance;
        rectTrs = GetComponent<RectTransform>();
    }
}
