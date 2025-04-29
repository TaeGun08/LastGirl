using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DirectionCamera : MonoBehaviour
{
    private GameManager gameManager;
    
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineTrackedDolly dolly;

    [SerializeField] private GameObject tpsCam;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        dolly.m_PathPosition = 0f;
        StartCoroutine(nameof(DirectionCameraCoroutine));
    }

    private IEnumerator DirectionCameraCoroutine()
    {
        while (dolly.m_PathPosition < 3.9f)
        {
            dolly.m_PathPosition = Mathf.Lerp(dolly.m_PathPosition, 4f, Time.deltaTime);
            yield return null;
        }
        
        tpsCam.SetActive(true);

        yield return new WaitForSeconds(2f);
        gameManager.IsGameStart = true;
        gameObject.SetActive(false);
    }
}
