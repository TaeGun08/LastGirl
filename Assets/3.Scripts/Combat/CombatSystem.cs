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
    
    private Dictionary<Collider, IDamageAble> hitAbleDic = new Dictionary<Collider, IDamageAble>();
    
    private Queue<InGameEvent> eventQueue = new Queue<InGameEvent>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        while (eventQueue.Count > 0)
        {
            InGameEvent e = eventQueue.Dequeue();
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

    public void AddHitAbleType(Collider collider, IDamageAble able)
    {
        hitAbleDic.TryAdd(collider, able);
    }

    public IDamageAble GetHitAble(Collider collider)
    {
        return hitAbleDic.GetValueOrDefault(collider);
    }

    public void AddEvent(InGameEvent e)
    {
        eventQueue.Enqueue(e);
    }
}
