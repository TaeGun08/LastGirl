using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCameraRotate : MonoBehaviour
{
    [Header("PlayerCameraRotate Settings")]
    [SerializeField, Range(0f, 1000f)] private float mouseSensitivity = 300f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachinePOV pov;
    private CinemachineFramingTransposer transposer;
    private CinemachineImpulseSource impulseSource;
    
    private LocalPlayer localPlayer;

    private void Awake()
    {
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        impulseSource =  GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void LateUpdate()
    {
        if (localPlayer.CCType.Equals(CrowdControlType.Unknown) == false) return;
        UpdateCameraZoom();
        UpdateWeaponRecoil();
        
        if (localPlayer.IsZoom)
        {
            pov.m_HorizontalAxis.m_MaxSpeed = mouseSensitivity * 0.5f;
            pov.m_VerticalAxis.m_MaxSpeed = mouseSensitivity * 0.5f;
        }
        else
        {
            pov.m_HorizontalAxis.m_MaxSpeed = mouseSensitivity;
            pov.m_VerticalAxis.m_MaxSpeed = mouseSensitivity;
        }
    }

    private void UpdateCameraZoom()
    {
        if (Input.GetMouseButton(1) && virtualCamera.m_Lens.FieldOfView > 30f)
        {
            Zoom(30f, 0.27f, Time.deltaTime * 5f);
            localPlayer.IsZoom = true;
        }
        else if (!Input.GetMouseButton(1) && virtualCamera.m_Lens.FieldOfView < 60f)
        {
            Zoom(60f, 0.37f, Time.deltaTime * 5f);
            localPlayer.IsZoom = false;
        }
    }

    private void Zoom(float targetFOV, float targetScreenX, float speed)
    {
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, speed);
        transposer.m_ScreenX = Mathf.Lerp(transposer.m_ScreenX, targetScreenX, speed);
    }

    private void UpdateWeaponRecoil()
    {
        if (!localPlayer.IsFire) return;
        float recoilX = localPlayer.IsZoom ? Random.Range(-0.005f, 0.005f) : Random.Range(-0.01f, 0.01f);
        float recoilY = localPlayer.IsZoom ? Random.Range(-0.005f, 0.005f) : Random.Range(-0.01f, 0.01f);
        float recoilZ = localPlayer.IsZoom ? Random.Range(-0.005f, 0.005f) : Random.Range(-0.01f, 0.01f);
        Vector3 recoil = new Vector3(recoilX,  recoilY, recoilZ);
        impulseSource.m_DefaultVelocity = recoil;
        impulseSource.GenerateImpulse();
    }
}
