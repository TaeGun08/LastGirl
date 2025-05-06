using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Store : MonoBehaviour
{
    protected AbilitySystem abilitySystem;
    protected RectTransform rectTrs;
    
    protected LocalPlayer localPlayer;
    
    [Header("Store Settings")] 
    [SerializeField] protected AbilityStore abilityStore;
    [SerializeField] protected AbilityUI abilityUI;
    
    protected virtual void Start()
    {
        abilitySystem = AbilitySystem.Instance;
        rectTrs = GetComponent<RectTransform>();
        
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
    }
}
