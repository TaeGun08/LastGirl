using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        AR,
    }
    
    [Header("GunData Setting")]
    public GunData Data;
    
    public abstract WeaponType Type { get;}
    
    private float currentFireDelay = 0.0f;
    
    [Header("Weapon Settings")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected ParticleSystem hitVFX;
    protected bool fireOn = false;
    
    private void Update()
    {
        currentFireDelay += Time.deltaTime;

        if (!(currentFireDelay >= Data.FireDelay)) return;
        currentFireDelay = 0.0f;
        Data.Ammo--;
        fireOn = true;
    }
    
    public abstract bool Fire();
}
