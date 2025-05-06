using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    
    private static readonly int IS_GAME_START = Animator.StringToHash("isGameStart");

    private AudioManager audioManager;
    
    [Header("Title Settings")] 
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject[] cams;
    [SerializeField] private Animator anim;
    public bool IsGameStart { get; set; }

    [Header("Lobby Settings")] 
    [SerializeField] private Transform playerTrs;
    [SerializeField] private GameObject lobbyUI;
    
    public bool IsGameReady { get; set; }

    public bool IsCameraTransition { get; set; }
    
    private void Awake()
    {
        Instance = this;
        
        buttons[0].onClick.AddListener(() =>
        {
            audioManager.SetSfxClip(audioManager.AudioObject.uiClips.ButtonsClips[0]);
            StartCoroutine(nameof(GameStartCoroutine));
        });
        
        buttons[1].onClick.AddListener(() =>
        {
            audioManager.SetSfxClip(audioManager.AudioObject.uiClips.ButtonsClips[0]);
            settingUI.gameObject.SetActive(true);
        });
        
        buttons[2].onClick.AddListener(() =>
        {
            audioManager.SetSfxClip(audioManager.AudioObject.uiClips.ButtonsClips[0]);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        });
    }

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    private IEnumerator GameStartCoroutine()
    {
        audioManager.SetBgmClip(audioManager.AudioObject.bgmClips.LobbyBgmClips[0]);
        playerTrs.DOMoveY(0.25f, 2f);
        anim.SetBool(IS_GAME_START, true);
        titleUI.gameObject.SetActive(false);
        cams[0].gameObject.SetActive(false);
        cams[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        IsGameStart = true;
        lobbyUI.gameObject.SetActive(true);
    }
}