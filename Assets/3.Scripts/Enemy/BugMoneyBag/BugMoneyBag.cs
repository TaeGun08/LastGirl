using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMoneyBag : Enemy
{
    protected override void UpdateMovement()
    {
        agent.SetDestination(localPlayer.transform.position);
    }

    protected override void UpdatePattern()
    {
        float distance = Vector3.Distance(transform.position, localPlayer.aimPoint.position);

        if (distance <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
