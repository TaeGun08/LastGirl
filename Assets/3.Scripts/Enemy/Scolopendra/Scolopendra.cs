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
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), 
            new Vector3(LocalPlayer.transform.position.x, 0f, LocalPlayer.transform.position.z));
        
        if (distance > 20) return;
        if (isPattern) return;
        attackDelay -= Time.deltaTime;
        if(attackDelay > 0 || currentState.Name.Equals(EnemyState.StateName.Walk)) return;
        isPattern = false;
        attackDelay = Data.AttackDelay;
        ChangeState(EnemyState.StateName.Attack);
    }
}
