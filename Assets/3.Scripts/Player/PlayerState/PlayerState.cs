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
    }

    protected Animator animator;
    protected LocalPlayer localPlayer;

    public abstract StateName Name { get;}

    protected virtual void Start()
    {
        localPlayer = Player.LocalPlayer.GetComponent<LocalPlayer>();
        animator = localPlayer.animator;
    }

    public abstract void StateEnter();
    public abstract void StateUpdate(PlayerController playerController);
    public abstract void StateExit();
}
