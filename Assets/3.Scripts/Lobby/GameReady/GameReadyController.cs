using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameReadyController : MonoBehaviour
{
    private MainManager mainManager;
    private AudioManager audioManager;
    
    [Header("InLobby Settings")] 
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private GameObject readyUI;
    [SerializeField] private GameObject[] cams;
    [SerializeField] private Button lobbyButton;
    
    [Header("InGameReady Settings")]
    [SerializeField] private Button inGameButton;
    [SerializeField] private Button[] mapButtons;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    private void Awake()
    {
        lobbyButton.onClick.AddListener(() =>
        {
            audioManager.SetSfxClip(audioManager.AudioObject.uiClips.ButtonsClips[0]);
            StartCoroutine(nameof(LobbyBackCoroutine));
        });
        
        inGameButton.onClick.AddListener(() =>
        {
            audioManager.SetSfxClip(audioManager.AudioObject.uiClips.ButtonsClips[0]);
            StartCoroutine(nameof(GameInCoroutine));
        });
    }

    private void Start()
    {
        mainManager = MainManager.Instance;
        audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        if (mainManager.IsCameraTransition) return; 
        if (mainManager.IsGameReady == false) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(nameof(LobbyBackCoroutine));
        }
    }

    private IEnumerator LobbyBackCoroutine()
    {
        readyUI.SetActive(false);
        cams[0].gameObject.SetActive(true);
        cams[1].gameObject.SetActive(false);
        mainManager.IsGameReady = false;
        mainManager.IsCameraTransition = true;
        yield return new WaitForSeconds(2f);
        mainManager.IsCameraTransition = false;
        lobbyUI.SetActive(true);
    }

    private IEnumerator GameInCoroutine()
    {
        lobbyButton.gameObject.SetActive(false);
        inGameButton.gameObject.SetActive(false);
        while (virtualCamera.m_Lens.FieldOfView <= 69f)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, 70f, Time.deltaTime * 5f);
            yield return null;
        }
        
        while (virtualCamera.m_Lens.FieldOfView > 1)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, 0f, Time.deltaTime * 8f);
            yield return null;
        }
        
        SceneManager.LoadSceneAsync("MechaBugsWorld");
    }
}
