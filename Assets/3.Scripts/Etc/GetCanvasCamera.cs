using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCanvasCamera : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }
}
