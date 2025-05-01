using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 25, LayerMask.GetMask("Enemy"));

        if (colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                agent.SetDestination(col.transform.position);
            }
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
}
