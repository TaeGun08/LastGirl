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
    }
    
    public AbilityData[] Data;

    public GameObject GetAbilityPrefab(int key)
    {
        GameObject prefab = null;

        foreach (AbilityData data in Data)
        {
            if (data.Key != key) continue;
            prefab = data.AbilityPrefab;
        }
        
        return prefab;
    }
}
