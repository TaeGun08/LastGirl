using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using TMPro;
using UnityEngine;

public class PlayerUIStatus : MonoBehaviour
{
    private LocalPlayer localPlayer;

    [Header("PlayerUI Settings")] 
    [SerializeField] private ProgressBar hpBar;
    [SerializeField] private ProgressBar durabilityBar;
    [SerializeField] private ProgressBar ammoBar;
    [SerializeField] private TMP_Text hasArca;

    private void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
    }

    private void LateUpdate()
    {
        hpBar.currentPercent = ((float)localPlayer.status.Hp / localPlayer.status.MaxHp) * 100f;
        durabilityBar.currentPercent = ((float)localPlayer.status.Durability / localPlayer.status.MaxDurability) * 100f;
        ammoBar.currentPercent = localPlayer.playerController.currentWeapon.Data.Ammo;
        ammoBar.suffix = $" / {localPlayer.playerController.currentWeapon.Data.MaxAmmo}";
        hasArca.text = $"{localPlayer.status.HasArca}";
    }
}
