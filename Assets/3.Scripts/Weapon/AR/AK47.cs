using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Weapon
{
    public override WeaponType Type => type;

    [Header("AK47 Settings")]
    [SerializeField] private WeaponType type;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    public override bool Fire()
    {
        if (fireOn == false) return false;
        
        muzzleFlash.Play();
        Ray ray = new  Ray(firePoint.transform.position, mainCam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Enemy")))
        {
            Debug.DrawRay(firePoint.transform.position, ray.direction * hit.distance, Color.red);
            Instantiate(hitVFX,  hit.point, Quaternion.LookRotation(hit.normal));
        }
        
        fireOn = false;
        
        return true;
    }
}
