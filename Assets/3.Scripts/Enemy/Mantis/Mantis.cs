using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantis : Enemy
{
    [Header("MinerBeetle Settings")] 
    [SerializeField] private GameObject[] attackColliders;
    [SerializeField] private float chaseAngle;
    [SerializeField] private int hasPattern;

    private void OnDrawGizmos()
    {
        Vector3 leftBoundary = Quaternion.Euler(0, -chaseAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, chaseAngle / 2, 0) * transform.forward;
        
        if (chaseAngle <= 180f)
        {
            Gizmos.color = Color.white;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * 5);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * 5);
        }
        else
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * 5);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * 5);
        }
    }
    
    protected override void UpdateMovement()
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z),
            new Vector3(localPlayer.transform.position.x, 0f, localPlayer.transform.position.z));

        if (distance <= 5f)
        {
            agent.SetDestination(transform.position);
            return;
        }

        agent.SetDestination(localPlayer.transform.position);
    }

    protected override void UpdatePattern()
    {
 
    }
}
