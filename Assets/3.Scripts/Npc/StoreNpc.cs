using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNpc : Npc
{
    private AbilityStore abilityStore;
    private StoreBuy storeBuy;
    private StoreShell storeShell;

    private void OnEnable()
    {
        if (storeBuy == null) return;
        storeBuy.OpenAbility();
        storeShell.HasAbility();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(nameof(OnAbilityStoreCoroutine));
    }

    private IEnumerator OnAbilityStoreCoroutine()
    {
        yield return null;
        hasUI = abilitySystem.AbilityCanvas.StoreUI;
        abilityStore = hasUI.GetComponentInParent<AbilityStore>();
        storeBuy = abilityStore.Buy;
        storeShell = abilityStore.Shell;
        storeBuy.OpenAbility();
    }
}