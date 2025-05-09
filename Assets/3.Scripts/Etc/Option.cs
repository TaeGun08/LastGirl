using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;
    private FadeInOut fadeIn;
    
    [Header("Option Settings")]
    [SerializeField] private Button[] buttons;

    [SerializeField] private GameObject settingUI;

    private void Awake()
    {
        ButtonsEvent();
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        fadeIn = FadeInOut.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == false
            || settingUI.activeInHierarchy == false) return;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameManager.IsOptionOpen = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void ButtonsEvent()
    {
        buttons[0].onClick.AddListener(() =>
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            audioManager.SetSfxClip(audioManager.AudioObject.uiClips.ButtonsClips[0]);
            gameManager.IsOptionOpen = false;
            Time.timeScale = 1;
            gameObject.SetActive(false);
        });
        
        buttons[1].onClick.AddListener(() =>
        {
            audioManager.SetSfxClip(audioManager.AudioObject.uiClips.ButtonsClips[0]);
            settingUI.SetActive(true);
        });
        
        buttons[2].onClick.AddListener(() =>
        {
            fadeIn.FadeOut("Main");
        });
    }
}
