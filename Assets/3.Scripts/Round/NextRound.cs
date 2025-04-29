using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRound : MonoBehaviour
{
    private RoundSystem roundSystem;

    private void Start()
    {
        roundSystem = RoundSystem.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;
        roundSystem.RoundCanvas.StartCountDown();
        gameObject.SetActive(false);
    }
}
