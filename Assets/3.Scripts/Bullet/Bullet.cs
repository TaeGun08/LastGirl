using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigid;
    
    [Header("Bullet Settings")] 
    [SerializeField] private float bulletSpeed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    public void AddForce(Vector3 force, ForceMode mode)
    {
        transform.forward = force;
        rigid.AddForce(force * bulletSpeed, mode);
    }
}
