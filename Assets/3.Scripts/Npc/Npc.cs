using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Npc : MonoBehaviour
{
    protected AbilitySystem abilitySystem;
    
    [Header("Npc Settings")] 
    [SerializeField] protected GameObject hasUI;
    protected bool isOpenUI = false;

    protected virtual void Start()
    {
        abilitySystem = AbilitySystem.Instance;
    }

    public void OpenUI()
    {
        isOpenUI = isOpenUI == false ? true : false;
        Cursor.visible = isOpenUI;
        Cursor.lockState = !isOpenUI ? CursorLockMode.Locked : CursorLockMode.None;
        hasUI.SetActive(isOpenUI);
    }
}
