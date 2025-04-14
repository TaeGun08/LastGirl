using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRotate : MonoBehaviour
{
    [Header("PlayerCameraRotate Settings")]
    [SerializeField] private Transform player;
    private Vector3 rotValue;
    
    private void Update()
    {
        UpdatePlayerRotation();
    }

    private void UpdatePlayerRotation()
    {
        player.rotation = Quaternion.Euler(transform.rotation.x, transform.eulerAngles.y, transform.rotation.z);
    }
}
