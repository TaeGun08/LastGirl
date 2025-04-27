using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UIShake : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(ShakeCoroutine));
    }

    private IEnumerator ShakeCoroutine()
    {
        rect.SetAsLastSibling();
        float timer = 1f;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            rect.anchoredPosition = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
            yield return null;
        }
        
        gameObject.SetActive(false);
    }
}
