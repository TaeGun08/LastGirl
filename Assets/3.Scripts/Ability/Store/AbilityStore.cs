using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AbilityStore : MonoBehaviour
{
    private AbilitySystem abilitySystem;
    private LocalPlayer localPlayer;
    public LocalPlayer LocalPlayer => localPlayer;
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;
    
    [Header("AbilityStore Settings")]
    [SerializeField] private AbilityObject abilityObject;
    public AbilityObject AbilityObject => abilityObject;
    [SerializeField] private StoreBuy buy;
    public StoreBuy Buy => buy;
    [SerializeField] private StoreShell shell;
    public StoreShell Shell => shell;
    public GameObject[] buyShellUI;
    [SerializeField] private ButtonManager[] buttons;
    public AbilityData AbilityData { get; set; }
    public GameObject AbilityUI { get; set; }
    [FormerlySerializedAs("confirmSellWarning")] [SerializeField] protected GameObject confirmBuyWarning;
    public GameObject ConfirmBuyWarning => confirmBuyWarning;
    
    private void Awake()
    {
        ButtonEvents();
    }

    private void Start()
    {
        abilitySystem = AbilitySystem.Instance;
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        playerController = localPlayer.playerController;
    }

    private void ButtonEvents()
    {
        buttons[0].onClick.AddListener(() =>
        {
            if (AbilityData == null) return;
            if (AbilityData.Price > localPlayer.status.HasArca) return;
            abilitySystem.SetAbility(AbilityData.Key);
            localPlayer.status.HasArca -= AbilityData.Price;
            AbilityUI.SetActive(false);
            buyShellUI[0].SetActive(false);
            shell.HasAbility();
            AbilityData = null;
            AbilityUI  = null;
        });
        
        buttons[1].onClick.AddListener(() =>
        {
            if (AbilityData == null) return;
            abilitySystem.RemoveAbility(AbilityData.Key);
            AbilityUI.SetActive(false);
            buyShellUI[1].SetActive(false);
            shell.HasAbility();
            AbilityData = null;
        });
        
        buttons[2].onClick.AddListener(() =>
        {
            buyShellUI[0].SetActive(false);
            AbilityData = null;
        });
        
        buttons[3].onClick.AddListener(() =>
        {
            buyShellUI[1].SetActive(false);
            AbilityData = null;
        });
    }
}
