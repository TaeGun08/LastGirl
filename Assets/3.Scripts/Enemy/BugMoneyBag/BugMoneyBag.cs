using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMoneyBag : Enemy
{
    [Header("BugMoneyBag Settings")] 
    [SerializeField] private ParticleSystem bombingParticle;
    
    protected override void UpdateMovement()
    {
        agent.SetDestination(localPlayer.transform.position);
    }

    protected override void UpdatePattern()
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0f , transform.position.z), 
            new Vector3(localPlayer.transform.position.x, 0f, localPlayer.transform.position.z));

        if (distance > 1f || isDead) return;
        isDead = true;
        StartCoroutine(BombingCoroutine());
    }

    private IEnumerator BombingCoroutine()
    {
        ParticleSystem particle = Instantiate(bombingParticle, transform.position, Quaternion.identity);
        particle.Play();
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
