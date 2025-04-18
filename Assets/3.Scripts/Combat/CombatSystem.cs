using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public static CombatSystem Instance;

    public class Callbacks
    {
        public Action<CombatEvent> OnCombatEvent;
    }
    
    public Dictionary<Collider, PartType> HitPartTypeDic = new Dictionary<Collider, PartType>();
    
    private void Awake()
    {
        Instance = this;
    }
}
