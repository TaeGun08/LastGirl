using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class UITest : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;

    [SerializeField, Range(0f, 100f)] private float value = 0f;

    public void Update()
    {
        progressBar.currentPercent = value;
    }
}
