using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arca : MonoBehaviour
{
    private Rigidbody rigid;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void AddForce(Vector3 force)
    {
        rigid.AddForce(force, ForceMode.Impulse);
    }
}
