using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class RoundCanvas : MonoBehaviour
{
    private RoundSystem roundSystem;
    
    [Header("RoundCanvas Settings")]
    [SerializeField] private TMP_Text roundText;

    public TMP_Text RoundText { get => roundText; set => roundText = value; }
    [SerializeField] private TMP_Text countDownText;

    private void Start()
    {
        roundSystem = RoundSystem.Instance;
        StartCountDown();
    }

    private IEnumerator CountDownCoroutine()
    {
        countDownText.gameObject.SetActive(true);
        float timer = 6f;
        while (timer > 1)
        {
            timer -= Time.deltaTime;
            countDownText.text = $"{(int)timer}";
            yield return null;
        }
        
        countDownText.text = $"스타트!";
        yield return new WaitForSeconds(1f);
        roundSystem.IsRoundStart = true;
        countDownText.gameObject.SetActive(false);
    }

    public void StartCountDown()
    {
        StartCoroutine(nameof(CountDownCoroutine));
    }
}
