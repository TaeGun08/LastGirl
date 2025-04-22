using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scolopendra : Enemy
{
    protected override void Update()
    {
        base.Update();
        currentState.StateUpdate();
    }
    
    protected override void UpdateMovement()
    {

    }

    protected override void UpdatePattern()
    {
        // if (isPattern) return;
        // attackDelay -= Time.deltaTime;
        // if(attackDelay > 0) return;
        // isPattern = false;
        // attackDelay = Data.AttackDelay;
        // ChangeState(EnemyState.StateName.Attack);
    }
}
