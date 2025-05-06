using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomFlyPoisonous : Enemy
{
    protected override void UpdateMovement()
    {
        Agent.SetDestination(LocalPlayer.transform.position);
    }

    protected override void UpdatePattern()
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0f , transform.position.z), 
            new Vector3(LocalPlayer.transform.position.x, 0f, LocalPlayer.transform.position.z));

        if (distance > 1f || isDead) return;
        isDead = true;
        Bombing();
    }

    private void Bombing()
    {
        audioManager.SetSfxClip(audioManager.AudioObject.enemyClips.DeadClips[0]);
        ParticleSystem particle = EffectPoolSystem.Instance.
            ParticlePool(IEffectPool.ParticleType.EnergyExplosionA).GetComponent<ParticleSystem>();
        particle.transform.position = transform.position;
        particle.Play();
        EnergyExplosion energyExplosion = particle.GetComponent<EnergyExplosion>();
        energyExplosion.Damage = Data.Damage;
        roundSystem.SpawnEnemyCount--;
        Debug.Log(roundSystem.SpawnEnemyCount);
        gameObject.SetActive(false);
    }
}
