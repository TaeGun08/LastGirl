using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerBeetle : Enemy
{
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
            new Vector3(LocalPlayer.transform.position.x, 0f, LocalPlayer.transform.position.z));

        if (distance <= 2f)
        {
            Agent.SetDestination(LocalPlayer.transform.position - transform.forward * 1.5f);
            return;
        }

        Agent.SetDestination(LocalPlayer.transform.position);
    }

    protected override void UpdatePattern()
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z),
            new Vector3(LocalPlayer.transform.position.x, 0f, LocalPlayer.transform.position.z));

        Vector3 direction = (LocalPlayer.transform.position - transform.position).normalized;
        float target = Vector3.Angle(transform.forward, direction);

        attackDelay -= Time.deltaTime;

        if (distance > 2f || target > chaseAngle * 0.5f || isDead || attackDelay > 0) return;
        Animator.SetTrigger(ATTACK);
        int ran = Random.Range(0, hasPattern);
        Animator.SetFloat(PATTERN, ran);
        isMoveStop = true;
        isPattern = true;
        StartCoroutine(PatternCoroutine(ran));
    }

    private IEnumerator PatternCoroutine(int pattern)
    {
        switch (pattern)
        {
            case 0:
                yield return new WaitForSeconds(0.7f);
                attackColliders[0].SetActive(true);
                yield return new WaitForSeconds(0.5f);
                break;
            case 1:
                yield return new WaitForSeconds(0.7f);
                attackColliders[0].SetActive(true);
                attackColliders[1].SetActive(true);
                yield return new WaitForSeconds(0.5f);
                break;
            case 2:
                yield return new WaitForSeconds(0.7f);
                attackColliders[1].SetActive(true);
                yield return new WaitForSeconds(0.5f);
                break;
            case 3:
            case 4:
                yield return new WaitForSeconds(1.2f);
                attackColliders[2].SetActive(true);
                yield return new WaitForSeconds(1f);
                break;
        }
        
        attackColliders[0].SetActive(false);
        attackColliders[1].SetActive(false);
        attackColliders[2].SetActive(false);
        
        yield return new WaitForSeconds(1f);
        isMoveStop = false;
        isPattern = false;
        attackDelay = Data.AttackDelay;
    }
}