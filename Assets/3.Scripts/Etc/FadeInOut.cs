using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut Instance;

    [SerializeField] private Image image;
    private string sceneName;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Color color = image.color;
        color.a = 1f;
        image.color = color;
        image.enabled = true;
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInOutCoroutine(true));
    }

    public void FadeOut(string sceneName)
    {
        this.sceneName = sceneName;
        StartCoroutine(FadeInOutCoroutine(false));
    }

    private IEnumerator FadeInOutCoroutine(bool fadeIn)
    {
        Time.timeScale = 0f;
        
        image.enabled = true;
        
        yield return new WaitForEndOfFrame();
        
        Color color = image.color;

        float targetFrame = 0f;
        float timer = 0f;
        
        while (targetFrame < 34)
        {
            targetFrame = 1 / Time.unscaledDeltaTime;
            timer += Time.unscaledDeltaTime;
            
            if (timer > 5f)
            {
                break;
            }
            
            yield return null;
        }
        
        while (gameObject.activeInHierarchy)
        {
            if (fadeIn)
            {
                color.a -= Time.unscaledDeltaTime * 0.5f;
                image.color = color;
                if (color.a <= 0f)
                {
                    color.a = 0f;
                    break;
                }
            }
            else
            {
                color.a += Time.unscaledDeltaTime * 0.5f;
                image.color = color;
                if (color.a >= 1f)
                {
                    color.a = 1f;
                    break;
                }
            }
            
            yield return null;
        }

        image.color = color;
        Time.timeScale = 1f;
        
        if (fadeIn)
        {
            image.enabled = false;
        }
        else
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
