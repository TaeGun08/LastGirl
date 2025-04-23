using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public enum StateName
    {
        Idle,
        Walk,
        Run,
        FireIdle,
        FireWalk,
        ReLoad,
        CrowdControl,
        Dash,
        Dead,
    }

    protected Animator animator;
    protected LocalPlayer localPlayer;
    protected PlayerController playerController;

    public abstract StateName Name { get;}

    protected virtual void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        animator = localPlayer.animator;
    }

    public abstract void StateEnter(PlayerController playerController);
    public abstract void StateUpdate();
    public abstract void StateExit();
}
