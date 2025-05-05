using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostumeManager : MonoBehaviour
{
    [Header("CostumeManager Settings")]
    [SerializeField] private PlayerCostume playerCostume;
    [Space]
    [SerializeField] private Button[] headButtons;
    [SerializeField] private Button[] faceButtons;
    [SerializeField] private Button[] bodyButtons;
    [SerializeField] private Button[] accButtons;
    
    private void Awake()
    {
        HeadButtonsEvent();
        FaceButtonsEvent();
        BodyButtonsEvent();
        AccButtonsEvent();
    }

    private void HeadButtonsEvent()
    {
        headButtons[0].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(0, 0);
        });
        
        headButtons[1].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(0, 1);
        });
    }

    private void FaceButtonsEvent()
    {
        faceButtons[0].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(1, 1);
        });
        
        faceButtons[1].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(1, 2);
        });
        
        faceButtons[2].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(1, 3);
        });
        
        faceButtons[3].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(1, 4);
        });
        
        faceButtons[4].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(1, 5);
        });
    }

    private void BodyButtonsEvent()
    {
        bodyButtons[0].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(2, 1);
        });
        
        bodyButtons[1].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(2, 2);
        });
        
        bodyButtons[2].onClick.AddListener(() =>
        {
            playerCostume.ChangeCostume(2, 3);
        });
    }

    private void AccButtonsEvent()
    {
        accButtons[0].onClick.AddListener(() =>
        {
            playerCostume.ChangeAccCostume(0, true);
        });
        
        accButtons[1].onClick.AddListener(() =>
        {
            playerCostume.ChangeAccCostume(1);
        });
        
        accButtons[2].onClick.AddListener(() =>
        {
            playerCostume.ChangeAccCostume(2);
        });
        
        accButtons[3].onClick.AddListener(() =>
        {
            playerCostume.ChangeAccCostume(3);
        });
        
        accButtons[4].onClick.AddListener(() =>
        {
            playerCostume.ChangeAccCostume(4);
        });
    }
}
