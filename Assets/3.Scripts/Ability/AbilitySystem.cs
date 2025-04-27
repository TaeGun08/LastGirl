using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public static AbilitySystem Instance;

    private LocalPlayer localPlayer;
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;

    public class Callbacks
    {
        public Action<CombatEvent> OnFireAbilityEvent;
        public Action<CombatEvent> OnDashAbilityEvent;
        public Action<CombatEvent> OnBarrierAbilityEvent;
        public Action<CombatEvent> OnPersistentAbilityEvent;
        public Action<CombatEvent> OnAutoAbilityEvent;
        public Action<CombatEvent> OnProjectileAbilityEvent;
    }
    
    [Header("Ability Settings")]
    [SerializeField] private AbilityObject abilityObject;
    [SerializeField] private AbilityCanvas abilityCanvas;
    public AbilityCanvas AbilityCanvas => abilityCanvas;
    
    public readonly Callbacks Events = new Callbacks();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        playerController = localPlayer.playerController;
    }
    
    private void AbilityCheck(Ability ability)
    {
        for (int i = 1; i < playerController.HasAbility.Length; i++)
        {
            if (playerController.HasAbility[i - 1] != null
                || playerController.HasAbility[i] == null) continue;
            playerController.HasAbility[i - 1] = playerController.HasAbility[i];
            playerController.HasAbility[i] = null;
        }
        
        for (int i = 0; i < playerController.HasAbility.Length; i++)
        {
            if (playerController.HasAbility[i] != null &&
                playerController.HasAbility[i].abilityData.Key.Equals(ability.abilityData.Key)) return;
            if (playerController.HasAbility[i] != null) continue;
            Debug.Log("생성");
            playerController.HasAbility[i] = ability;
            return;
        }
        
        Debug.Log("능력이 6개가 모두 존재합니다.");
    }

    public void SetAbility(int key)
    {
        Ability ability = Instantiate(abilityObject.GetAbilityPrefab(key).AbilityPrefab, transform)
            .GetComponent<Ability>();
        ability.abilityData = abilityObject.GetAbilityPrefab(key);
        AbilityCheck(ability);
    }

    public void RemoveAbility(int key)
    {
        for (int i = 0; i < playerController.HasAbility.Length; i++)
        {
            if (playerController.HasAbility[i] == null) continue;
            if (playerController.HasAbility[i].abilityData.Key.Equals(key) == false) continue;
            Debug.Log($"{playerController.HasAbility[i].abilityData.Name}능력을(를) 버렸습니다.");
            localPlayer.status.HasArca += (int)(playerController.HasAbility[i].abilityData.Price * 0.3f);
            Destroy(playerController.HasAbility[i].gameObject);
            playerController.HasAbility[i] = null;
            break;
        }
        
        for (int i = 1; i < playerController.HasAbility.Length; i++)
        {
            if (playerController.HasAbility[i - 1] != null
                || playerController.HasAbility[i] == null) continue;
            playerController.HasAbility[i - 1] = playerController.HasAbility[i];
            playerController.HasAbility[i] = null;
        }
    }

    public bool HasAbility(int key)
    {
        for (int index = 0; index < playerController.HasAbility.Length; index++)
        {
            Ability hasAbility = playerController.HasAbility[index];
            if (hasAbility == null) continue;
            if (hasAbility.abilityData.Key.Equals(key)) return true;
        }

        return false;
    }
}
