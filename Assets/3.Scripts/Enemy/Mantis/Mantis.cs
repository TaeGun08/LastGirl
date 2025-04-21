using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantis : Enemy
{
    private static readonly int IS_WALK = Animator.StringToHash("isWalk");

    [Header("MinerBeetle Settings")] 
    [SerializeField] private GameObject[] attackColliders;
    [SerializeField] private float chaseAngle;
    [SerializeField] private int hasPattern;
    private bool isWalk;
    
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

        if (distance <= 7f)
        {
            agent.SetDestination(localPlayer.transform.position - transform.forward * 6.5f);
            animator.SetBool(IS_WALK, false);
            return;
        }

        agent.SetDestination(localPlayer.transform.position);
        animator.SetBool(IS_WALK, true);
    }

    protected override void UpdatePattern()
    {
        
    }
}
