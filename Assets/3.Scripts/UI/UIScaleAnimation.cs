using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIScaleAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOScale(Vector3.one, 0.2f).OnComplete(() => DOTween.Kill(this));
    }

    private void OnDisable()
    {
        transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => DOTween.Kill(this));
    }
}
