using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class PlayerCameraRotate : MonoBehaviour
{
    [Header("PlayerCameraRotate Settings")]
    [SerializeField, Range(0f, 1000f)] private float mouseSensitivity = 300f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachinePOV pov;
    private CinemachineFramingTransposer framingTransposer;
    
    private LocalPlayer localPlayer;

    private void Awake()
    {
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateCameraZoom();
        UpdateWeaponRecoil();
    }

    private void LateUpdate()
    {
        pov.m_HorizontalAxis.m_MaxSpeed = mouseSensitivity;
        pov.m_VerticalAxis.m_MaxSpeed = mouseSensitivity;
    }

    private void UpdateCameraZoom()
    {
        if (Input.GetMouseButton(1) && virtualCamera.m_Lens.FieldOfView > 30f)
        {
            Zoom(30f, 0.4f,  Time.deltaTime * 5f);
            localPlayer.IsZoom = true;
        }
        else if (!Input.GetMouseButton(1) && virtualCamera.m_Lens.FieldOfView < 60f)
        {
            Zoom(60f, 0.5f,  Time.deltaTime * 5f);
            localPlayer.IsZoom = false;
        }
    }

    private void Zoom(float targetFOV, float targetOffsetX, float speed)
    {
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, speed);
        framingTransposer.m_TrackedObjectOffset.x = 
            Mathf.Lerp(framingTransposer.m_TrackedObjectOffset.x, targetOffsetX, speed);
    }

    private void UpdateWeaponRecoil()
    {
        if (localPlayer.IsShotReady)
        {
            transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
        }
    }
}
