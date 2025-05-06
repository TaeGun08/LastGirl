using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private FadeInOut fadeInOut;
    
    [Header("GameEnd Settings")]
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        buttons[0].onClick.AddListener(() =>
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            fadeInOut.FadeOut("MechaBugsWorld");
            Time.timeScale = 1;
        });
        
        buttons[1].onClick.AddListener(() =>
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            fadeInOut.FadeOut("Main");
            Time.timeScale = 1;
        });
    }

    private void Start()
    {
        fadeInOut = FadeInOut.Instance;
    }
}
