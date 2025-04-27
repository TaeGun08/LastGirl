using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityData
{
    public enum AbilituRank
    {
        F,
        C,
        B,
        A,
        S,
    }
    
    public int Key;
    public string Name;
    public AbilituRank Rank;
    public int Price;
    public GameObject AbilityPrefab;
    public string AbilitySummary;
}

[CreateAssetMenu(fileName = "AbilityObject", menuName = "Ability/AbilityObject")]
public class AbilityObject : ScriptableObject
{
    public AbilityData[] Data;

    public AbilityData GetAbilityPrefab(int key)
    {
        AbilityData returnData = null;

        foreach (AbilityData data in Data)
        {
            if (data.Key != key) continue;
            returnData = data;
        }
        
        return returnData;
    }
}
