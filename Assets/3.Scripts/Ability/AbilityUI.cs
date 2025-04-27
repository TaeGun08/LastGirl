using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbilityUI : MonoBehaviour
{
    protected AbilityStore abilityStore;
    
    [Header("AbilityUI Settings")]
    [SerializeField] protected TMP_Text abilityName;
    [SerializeField] protected TMP_Text abilitySummary;
    [SerializeField] protected TMP_Text abilityRnak;
    public AbilityData AbilityData { get; set; }
    public RectTransform PrevRectTrs { get; set; }
    protected RectTransform rectTrs;
    protected GraphicRaycaster raycaster;

    private void Awake()
    {
        rectTrs = GetComponent<RectTransform>();
    }

    protected void Start()
    {
        abilityStore = GetComponentInParent<AbilityStore>();
        raycaster = AbilitySystem.Instance.AbilityCanvas.GetComponent<GraphicRaycaster>();
    }

    protected void OnEnable()
    {
        SetAbilityData();
    }

    public virtual void SetAbilityData()
    {
        if (AbilityData == null) return;
        abilityName.text = AbilityData.Name;
        abilitySummary.text = AbilityData.AbilitySummary;
        abilityRnak.text = $"등급 : {AbilityData.Rank.ToString()}";
    }
}
