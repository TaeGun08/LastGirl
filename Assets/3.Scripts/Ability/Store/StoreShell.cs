using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoreShell : Store
{
    private List<HasAbilityUI> hasAbilityUIPrefabs = new List<HasAbilityUI>();
    [SerializeField] private RectTransform[] abilityUIRect;

    private void OnEnable()
    {
        HasAbility();
    }

    public void HasAbility()
    {
        if (hasAbilityUIPrefabs.Count <= 0)
        {
            for (int i = 0; i < 6; i++)
            {
                hasAbilityUIPrefabs.Add(Instantiate(abilityUI, abilityUIRect[i].position, Quaternion.identity,
                    abilityStore.transform.GetChild(0)).GetComponent<HasAbilityUI>());
                hasAbilityUIPrefabs[i].targetRectTrs = abilityStore.Shell.GetComponent<RectTransform>();
                hasAbilityUIPrefabs[i].PrevRectTrs = abilityUIRect[i];
                hasAbilityUIPrefabs[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < hasAbilityUIPrefabs.Count; i++)
        {
            hasAbilityUIPrefabs[i].gameObject.SetActive(false);

            if (abilitySystem == null) continue;
            if (abilitySystem.PlayerController.HasAbility[i] == null) continue;
            hasAbilityUIPrefabs[i].AbilityData = abilitySystem.PlayerController.HasAbility[i].abilityData;
            hasAbilityUIPrefabs[i].gameObject.SetActive(true);
            hasAbilityUIPrefabs[i].SetAbilityData();
        }
    }
}