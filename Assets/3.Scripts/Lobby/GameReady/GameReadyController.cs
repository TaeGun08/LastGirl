using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameReadyController : MonoBehaviour
{
    private MainManager mainManager;

    [Header("InLobby Settings")] 
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private GameObject readyUI;
    [SerializeField] private GameObject[] cams;
    [SerializeField] private Button lobbyButton;
    
    [Header("InGameReady Settings")]
    [SerializeField] private Button inGameButton;
    [SerializeField] private Button[] mapButtons;
    
    private void Awake()
    {
        lobbyButton.onClick.AddListener(() =>
        {
            StartCoroutine(nameof(LobbyBackCoroutine));
        });
        
        inGameButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("MechaBugsWorld");
        });
    }

    private void Start()
    {
        mainManager = MainManager.Instance;
    }

    private void Update()
    {
        if (mainManager.IsGameReady == false) return;
        
        
    }

    private IEnumerator LobbyBackCoroutine()
    {
        readyUI.SetActive(false);
        yield return new WaitForSeconds(2f);
        mainManager.IsGameReady = false;
        lobbyUI.SetActive(true);
    } 
}
