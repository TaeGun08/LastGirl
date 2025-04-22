using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public enum StateName
    {
        Idle,
        Walk,
        Attack,
        Dead,
    }

    public abstract StateName Name { get; }
    public Enemy enemy { get; set; }

    public abstract void StateEnter(Enemy enemy);
    
    public abstract void StateUpdate();
    
    public abstract void StateExit();
}
