using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        float ranRecoil = Random.Range(-0.3f, 0.3f);
        Vector3 aimpoint = localPlayer.aimPoint.position;
        aimpoint.x += (localPlayer.IsZoom ? 0f : ranRecoil);
        aimpoint.y += (localPlayer.IsZoom ? 0f : ranRecoil);
        Vector3 dir = (aimpoint - firePoint.position).normalized;
        Ray ray = new Ray(firePoint.position, dir);
        CombatEvent e =  new CombatEvent();
        
        int layer = LayerMask.GetMask("Enemy", "Map");
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, layer))
        {
            Debug.DrawRay(firePoint.position, ray.direction * hit.distance, Color.red);
            GameObject obj = effectPoolSystem.ParticlePool(IEffectPool.ParticleType.HitA).gameObject;
            obj.transform.position = hit.point;
            obj.transform.rotation = Quaternion.LookRotation(hit.normal);
            
            IDamageAble hitAble = CombatSystem.Instance.GetHitAble(hit.collider);
            if (hitAble != null)
            {
                e.Sender = localPlayer;
                e.Receiver = hitAble;
                e.Damage = Data.Damage;
                e.Collider = hit.collider;
                e.HitPosition = hit.point;
                e.HasParts = hitAble.HasParts;
                
                CombatSystem.Instance.AddEvent(e);
            }
        }
        
        e.FirePoint = FirePoint;
        AbilitySystem.Instance.Events.OnFireAbilityEvent?.Invoke(e);
        fireOn = false;
        localPlayer.IsFire = true;
        
        return true;
    }
}
