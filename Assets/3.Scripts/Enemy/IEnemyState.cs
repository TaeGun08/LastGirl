using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    public void StateEnter(Enemy enemy);
    
    public void StateUpdate();
    
    public void StateExit();
}
