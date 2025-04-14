using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCam : MonoBehaviour
{
    private LocalPlayer localPlayer;

    private void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
    }

    private void LateUpdate()
    {
        transform.LookAt(localPlayer.camLookAtPoint);
        transform.Translate(localPlayer.camLookAtPoint.position * Time.deltaTime, Space.World);
    }
}
