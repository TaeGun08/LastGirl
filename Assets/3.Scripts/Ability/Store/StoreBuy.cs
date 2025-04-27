using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class StoreBuy : Store
{
    private List<AbilityData> abilityDatas = new List<AbilityData>();
    [SerializeField] private AbilityData[] abilityData = new AbilityData[3];
    [SerializeField] private RectTransform[] abilityUIRect;
    private List<StoreAbilityUI> abilityUIPrefab = new List<StoreAbilityUI>();

    private void OnEnable()
    {
        if (abilityUIPrefab.Count <= 0) return;
        for (int i = 0; i < abilityUIPrefab.Count; i++)
        {
            abilityUIPrefab[i].transform.position = abilityUIRect[i].position;
            abilityUIPrefab[i].transform.SetAsLastSibling();
        }
    }

    public void OpenAbility()
    {
        abilityDatas.Clear();
        
        foreach (AbilityData data in abilityStore.AbilityObject.Data)
        {
            abilityDatas.Add(data);
        }

        foreach (Ability ability in abilityStore.PlayerController.HasAbility)
        {
            if (ability == null) continue;

            for (int j = 0; j < abilityDatas.Count; j++)
            {
                if (ability.abilityData.Key.Equals(abilityDatas[j].Key) == false) continue;
                abilityDatas.RemoveAt(j);
                break;
            }
        }

        for (int i = 0; i < abilityData.Length; i++)
        {
            for (int j = 0; j < abilityDatas.Count; j++)
            {
                if (i == 0) break;
                if (abilityData[i - 1].Key.Equals(abilityDatas[j].Key) == false) continue;
                abilityDatas.RemoveAt(j);
                break;
            }

            if (abilityDatas.Count <= 0) return;
            abilityData[i] = abilityDatas[Random.Range(0, abilityDatas.Count)];

            if (abilityUIPrefab.Count < 3)
            {
                abilityUIPrefab.Add(Instantiate(abilityUI, abilityUIRect[i].position, Quaternion.identity,
                    abilityStore.transform.GetChild(0)).GetComponent<StoreAbilityUI>());
                abilityUIPrefab[i].targetRectTrs = abilityStore.Buy.GetComponent<RectTransform>();
                abilityUIPrefab[i].PrevRectTrs = abilityUIRect[i];
            }

            abilityUIPrefab[i].AbilityData = abilityData[i];
            abilityUIPrefab[i].SetAbilityData();
            if (abilitySystem == null) continue;
            if (abilitySystem.HasAbility(abilityData[i].Key)) continue;
            abilityUIPrefab[i].gameObject.SetActive(true);
        }
    }

    public void RefreshAbility()
    {
    }
}