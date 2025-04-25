using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityObject", menuName = "Ability/AbilityObject")]
public class AbilityObject : ScriptableObject
{
    public enum AbilituRank
    {
        FRank,
        CRank,
        BRank,
        ARank,
        SRank,
    }
    
    [System.Serializable]
    public class AbilityData
    {
        public int Key;
        public string Name;
        public AbilituRank Rank;
        public GameObject AbilityPrefab;
        public string AbilitySummary;
    }
    
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
