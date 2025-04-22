using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RotateCoroutine());
        }
    }

    private IEnumerator RotateCoroutine()
    {
        Vector3 eulerAngles = transform.eulerAngles;
        float targetAngle = eulerAngles.y + 180f;
        
        while (eulerAngles.y < targetAngle)
        {
            eulerAngles.y += 180f * Time.deltaTime;
            transform.eulerAngles = eulerAngles;
            
            yield return null;
        }
        
        eulerAngles.y = eulerAngles.y >= 360f ? 0f :180f;
        
        transform.eulerAngles = eulerAngles;
    }
}
