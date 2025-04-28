using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData
{
    public Enemy[] Minion;
    public Enemy[] Elite;
    public Enemy[] SubBoss;
    public Enemy[] LastBoss;
    public int MaxSpawnMinion;
    public int MaxSpawnElite;
    public int MaxSpawnSubBoss;
    public int MaxSpawnLastBoss;
}

[CreateAssetMenu(fileName = "RoundObject", menuName = "Round/RoundObject")]
public class RoundObject : ScriptableObject
{
    public RoundData[] Data;
}
