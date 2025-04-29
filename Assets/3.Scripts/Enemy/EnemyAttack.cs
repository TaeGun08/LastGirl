using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected PlayerCamera playerCam;

    [Header("Attack Effects")]
    [SerializeField] protected ParticleSystem[] particles;
    protected void Start()
    {
        playerCam = Camera.main.GetComponent<PlayerCamera>();
    }
    
    public abstract IEnumerator Pattern(Enemy enemy);
}
