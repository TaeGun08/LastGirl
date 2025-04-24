using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] private float speed;
    
    private void Update()
    {
        transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
    }
}
