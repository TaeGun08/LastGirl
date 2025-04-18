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
    
    private Dictionary<Collider, IDamageAble> HitAbleDic = new Dictionary<Collider, IDamageAble>();
    private Dictionary<Collider, PartType> HitPartTypeDic = new Dictionary<Collider, PartType>();
    
    private Queue<InGameEvent> EventQueue = new Queue<InGameEvent>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        while (EventQueue.Count > 0)
        {
            InGameEvent e = EventQueue.Dequeue();
            switch (e.Type)
            {
                case InGameEvent.EventType.Combat:
                    CombatEvent combatEvent = e as CombatEvent;
                    e.Receiver.TakeDamage(combatEvent);
                    break;
                case InGameEvent.EventType.Heal:
                    break;
                case InGameEvent.EventType.Unknown:
                    break;
            }
        }
    }

    public IDamageAble GetHitPartType(Collider collider)
    {
        HitPartTypeDic[collider] = PartType.Unknown;
        return HitAbleDic.ContainsKey(collider) ? HitAbleDic[collider] : null;
    }

    public void AddHitPartType(Collider collider, IDamageAble type)
    {
        HitAbleDic.Add(collider, type);
    }

    public void AddEvent(InGameEvent e)
    {
        EventQueue.Enqueue(e);
    }
}
