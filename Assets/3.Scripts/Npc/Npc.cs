using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Npc : MonoBehaviour
{
    protected AbilitySystem abilitySystem;
    protected PlayerController playerController;
    protected PlayerCamera playerCamera;
    
    [Header("Npc Settings")] 
    [SerializeField] protected GameObject hasUI;
    protected bool isOpenUI;

    protected virtual void Start()
    {
        abilitySystem = AbilitySystem.Instance;
        playerController = Player.LocalPlayer.playerController;
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
    }

    public void OpenUI()
    {
        isOpenUI = isOpenUI == false ? true : false;
        Cursor.visible = isOpenUI;
        Cursor.lockState = !isOpenUI ? CursorLockMode.Locked : CursorLockMode.None;
        hasUI.SetActive(isOpenUI);
        abilitySystem.IsOpenStore = isOpenUI;
        playerController.ChangeState(PlayerState.StateName.Idle);
        playerCamera.VirtualCamera.gameObject.SetActive(!isOpenUI);
    }
}
