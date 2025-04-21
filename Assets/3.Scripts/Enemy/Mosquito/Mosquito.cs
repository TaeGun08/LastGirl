using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : Enemy
{
    [Header("Mosquito Settings")]
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

        if (distance < 2f)
        {
            agent.SetDestination(localPlayer.transform.position - transform.forward * 3f);
            return;
        }
        
        agent.SetDestination(localPlayer.transform.position);
    }

    protected override void UpdatePattern()
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z),
            new Vector3(localPlayer.transform.position.x, 0f, localPlayer.transform.position.z));

        Vector3 direction = (localPlayer.transform.position - transform.position).normalized;
        float target = Vector3.Angle(transform.forward, direction);
        
        attackDelay -= Time.deltaTime;
        
        if (distance > 2f || target > chaseAngle * 0.5f || isDead || attackDelay > 0) return;
        animator.SetTrigger(ATTACK);
        int ran = Random.Range(0, hasPattern);
        animator.SetFloat(PATTERN, ran);
        isMoveStop = true;
        isPattern = true;
        StartCoroutine(PatternCoroutine(ran));
    }

    private IEnumerator PatternCoroutine(int pattern)
    {
        yield return new WaitForSeconds(0.2f);
        
        switch (pattern)
        {
            case 0:
                attackColliders[0].SetActive(true);
                break;
            case 1:
                attackColliders[1].SetActive(true);
                yield return new WaitForSeconds(0.5f);
                attackColliders[0].SetActive(true);
                break;
            case 2:
                attackColliders[1].SetActive(true);
                break;
        }
        
        yield return new WaitForSeconds(1f);
        attackColliders[0].SetActive(false);
        attackColliders[1].SetActive(false);
        yield return new WaitForSeconds(1f);
        attackDelay = Data.AttackDelay;
        isMoveStop = false;
        isPattern = false;
        attackDelay = Data.AttackDelay;
    }
}