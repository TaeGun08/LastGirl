using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEmergency : MonoBehaviour
{
    private Camera mainCam;
    
    [Header("PlayerEmergency")]
    [SerializeField] private Image[] emergencyImages;

    [SerializeField] private Color prevColor;
    [SerializeField] private Color targetColor;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(Player.LocalPlayer.transform.position, 
            20f, LayerMask.GetMask("TargetEmergency"));


        if (colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                Transform monster = col.transform;
                Vector3 viewportPos = mainCam.WorldToViewportPoint(monster.position);

                EmergencyLerp(viewportPos.z < 0, 1);
                if (viewportPos.z <= 0) continue;
                EmergencyLerp(viewportPos.y > 1, 0);
                EmergencyLerp(viewportPos.y < 0, 1);
                EmergencyLerp(viewportPos.x < 0, 2);
                EmergencyLerp(viewportPos.x > 1, 3);
            }
        }
        else
        {
            EmergencyLerp(false, 0);
            EmergencyLerp(false, 1);
            EmergencyLerp(false, 2);
            EmergencyLerp(false, 3);
        }
    }

    private void EmergencyLerp(bool target, int index)
    {
        emergencyImages[index].gameObject.SetActive(target);
    }
}
