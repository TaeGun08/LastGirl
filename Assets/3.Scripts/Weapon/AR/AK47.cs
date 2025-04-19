using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Weapon
{
    public override WeaponType Type => type;

    private EffectPoolSystem effectPoolSystem;
    
    [Header("AK47 Settings")]
    [SerializeField] private WeaponType type;

    protected override void Start()
    {
        base.Start();
        effectPoolSystem = EffectPoolSystem.Instance;
    }
    
    public override bool Fire()
    {
        if (fireOn == false) return false;
        
        muzzleFlash.Play();
        Data.Ammo--;

        Vector3 dir = (localPlayer.aimPoint.position - firePoint.position).normalized;
        Ray ray = new  Ray(firePoint.position, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, 200f))
        {
            Debug.DrawRay(firePoint.position, ray.direction * hit.distance, Color.red);
            GameObject obj = effectPoolSystem.ParticlePool(IEffectPool.ParticleType.HitA).gameObject;
            obj.transform.position = hit.point;
            obj.transform.rotation = Quaternion.LookRotation(hit.normal);
            
            var hitAble = CombatSystem.Instance.GetHitAble(hit.collider);
            if (hitAble != null)
            {
                CombatEvent e =  new CombatEvent();
                e.Sender = localPlayer;
                e.Receiver = hitAble;
                e.Damage = Data.Damage;
                e.Collider = hit.collider;
                e.HitPosition = hit.point;
                e.HasParts = hitAble.HasParts;
                
                CombatSystem.Instance.AddEvent(e);
            }
        }
        
        fireOn = false;
        localPlayer.IsFire = true;
        
        return true;
    }
}
