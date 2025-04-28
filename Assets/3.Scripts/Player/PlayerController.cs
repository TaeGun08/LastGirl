using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    
    private static readonly int RELOAD = Animator.StringToHash("Reload");
    public LocalPlayer LocalPlayer { get; private set; }

    [Header("PlayerController Settings")] [SerializeField]
    private PlayerState[] states;

    private PlayerState currentState;
    [SerializeField] private Transform weaponTrs;

    public Transform WeaponTrs => weaponTrs;

    public CharacterController CharacterController { get; private set; }

    private Dictionary<PlayerState.StateName, PlayerState> playerStatesDic
        = new Dictionary<PlayerState.StateName, PlayerState>();
    
    [SerializeField] private Weapon[] weapons;
    public Weapon currentWeapon { get; private set; }

    [SerializeField] private Ability[] hasAbility = new Ability[6];
    public Ability[] HasAbility { get => hasAbility; set => hasAbility = value; }
    
    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        
        LocalPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();

        for (int i = 0; i < states.Length; i++)
        {
            playerStatesDic.Add(states[i].Name, states[i]);
            states[i].gameObject.SetActive(false);
        }

        currentState = states[0];
        currentState.StateEnter(this);
        states[0].gameObject.SetActive(true);
        
        currentWeapon = weapons[0];
    }

    private void Update()
    {
        if (gameManager.IsGameStart == false ) return;
        
        currentState.StateUpdate();
        OnNpcCheck();
    }

    public void ChangeState(PlayerState.StateName changeState)
    {
        currentState.StateExit();
        currentState = playerStatesDic[changeState];
        currentState.gameObject.SetActive(true);
        currentState.StateEnter(this);
    }

    public void ReloadWeapon(Vector2 inputAxis)
    {
        AnimatorStateInfo stateInfo = LocalPlayer.animator.GetCurrentAnimatorStateInfo(2);

        if (stateInfo.IsName("Reload") && stateInfo.normalizedTime < 0.85f) return;
        
        if (!Input.GetKeyDown(KeyCode.R) || currentWeapon.Data.Ammo.Equals(currentWeapon.Data.MaxAmmo)) return;
        
        StartCoroutine(ReloadCoroutine(inputAxis));
    }

    private IEnumerator ReloadCoroutine(Vector2 inputAxis)
    {
        LocalPlayer.IsReload = true;
        ChangeState(inputAxis == Vector2.zero ? PlayerState.StateName.FireIdle: PlayerState.StateName.FireWalk);
        LocalPlayer.animator.ResetTrigger(RELOAD);
        LocalPlayer.animator.SetTrigger(RELOAD);
        weaponTrs.localRotation = Quaternion.identity;
        
        yield return new WaitForSeconds(1f);
        
        LocalPlayer.IsReload = false;
        LocalPlayer.animator.ResetTrigger(RELOAD);
        LocalPlayer.animator.SetLayerWeight(2, 0f);
        currentWeapon.Data.Ammo = currentWeapon.Data.MaxAmmo;
        ChangeState(currentState.Name);
    }

    private void OnNpcCheck()
    {
        if(Input.GetKeyDown(KeyCode.F) == false) return;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up, 1.5f,  
            LayerMask.GetMask("Npc"));
        
        if (colliders.Length <= 0) return;
        
        foreach (Collider collider in colliders)
        {
            collider.gameObject.GetComponent<Npc>().OpenUI();
        }
    }
}