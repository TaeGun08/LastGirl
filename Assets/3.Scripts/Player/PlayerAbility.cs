using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    [SerializeField] private AbilityObject abilityObject;
    [SerializeField] private LocalPlayer localPlayer;
    
    private void Start()
    {
        localPlayer.status.Abilities[0] = Instantiate(abilityObject.GetAbilityPrefab(0)).GetComponent<Ability>();
    }
}
