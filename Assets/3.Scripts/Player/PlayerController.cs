using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LocalPlayer LocalPlayer { get; private set; }

    [Header("PlayerController Settings")] [SerializeField]
    private PlayerState[] states;

    private PlayerState currentState;
    [SerializeField] private Transform weaponTrs;

    public Transform WeaponTrs => weaponTrs;

    public CharacterController CharacterController { get; private set; }

    private Dictionary<PlayerState.StateName, PlayerState> playerStatesDic
        = new Dictionary<PlayerState.StateName, PlayerState>();
    
    [SerializeField] private Weapon[] weapons;
    public Weapon currentWeapon { get; private set; }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        LocalPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();

        for (int i = 0; i < states.Length; i++)
        {
            playerStatesDic.Add(states[i].Name, states[i]);
            states[i].gameObject.SetActive(false);
        }

        currentState = states[0];
        states[0].gameObject.SetActive(true);
        
        currentWeapon = weapons[0];
    }

    private void Update()
    {
        currentState.StateUpdate(this);
    }

    public void ChangeState(PlayerState.StateName changeState)
    {
        currentState.StateExit();
        currentState = playerStatesDic[changeState];
        currentState.gameObject.SetActive(true);
        currentState.StateEnter();
    }
}