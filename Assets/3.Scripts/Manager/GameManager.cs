using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private AudioManager audioManager;

    public bool IsGameStart { get; set; }
    public bool IsGameEnd { get; set; }

    [Header("GameOver Settings")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject[] gameEndTexts;
    
    [Header("Option")]
    [SerializeField] private GameObject optionUI;
    public bool IsOptionOpen { get; set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    public void GameEnd(bool isGameClear)
    {
        StartCoroutine(GameOverCoroutine(isGameClear));
    }

    private IEnumerator GameOverCoroutine(bool isGameClear)
    {
        IsGameEnd = true;
        yield return new WaitForSeconds(2f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        audioManager.SetBgmClip(isGameClear ? audioManager.AudioObject.bgmClips.GameBgmClips[2] 
            : audioManager.AudioObject.bgmClips.GameBgmClips[1]);
        gameOverUI.gameObject.SetActive(true);
        gameEndTexts[isGameClear ? 1 : 0].SetActive(true);
    }

    public void Option()
    {
        IsOptionOpen = IsOptionOpen == false;
        Cursor.visible = IsOptionOpen;
        Cursor.lockState = IsOptionOpen ? CursorLockMode.None : CursorLockMode.Locked;
        optionUI.SetActive(IsOptionOpen);
    }
}
