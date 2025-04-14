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
    }

    protected Animator animator;

    public abstract StateName Name { get;}

    protected void Start()
    {
        this.animator = Player.LocalPlayer.animator;
    }

    public abstract void StateEnter();
    public abstract void StateUpdate(PlayerController playerController);
    public abstract void StateExit();
}
