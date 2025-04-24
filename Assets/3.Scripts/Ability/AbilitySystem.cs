using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public static AbilitySystem Instance;

    private LocalPlayer localPlayer;
    private PlayerController playerController;

    public class Callbacks
    {
        public Action<CombatEvent> OnFireAbilityEvent;
        public Action<CombatEvent> OnDashAbilityEvent;
        public Action<CombatEvent> OnBarrierAbilityEvent;
        public Action<CombatEvent> OnPersistentAbilityEvent;
        public Action<CombatEvent> OnAutoAbilityEvent;
    }
    
    [Header("Ability Settings")]
    [SerializeField] private AbilityObject abilityObject;
    
    public readonly Callbacks Events = new Callbacks();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        playerController = localPlayer.playerController;
        
        BuyAbility(0);
        BuyAbility(1);
        BuyAbility(2);
        BuyAbility(3);
        BuyAbility(4);
    }

    public void BuyAbility(int key)
    {
        Ability ability = Instantiate(abilityObject.GetAbilityPrefab(key).AbilityPrefab, transform)
            .GetComponent<Ability>();
        SetAbility(ability);
    }
    
    public void SetAbility(Ability ability)
    {
        for (int i = 0; i < playerController.HasAbility.Length; i++)
        {
            if (playerController.HasAbility[i] != null &&
                playerController.HasAbility[i].Data.key.Equals(ability.Data.key)) return;
            if (playerController.HasAbility[i] != null) continue;
            playerController.HasAbility[i] = ability;
            return;
        }
        
        Debug.Log("능력이 6개가 모두 존재합니다.");
    }

    public void RemoveAbility(int index)
    {
        Debug.Log($"{playerController.HasAbility[index].gameObject}능력을(를) 버렸습니다.");
        Destroy(playerController.HasAbility[index].gameObject);
        playerController.HasAbility[index] = null;
    }
}
