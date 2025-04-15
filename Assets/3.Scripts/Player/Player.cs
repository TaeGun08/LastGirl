using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player LocalPlayer;

    [System.Serializable]
    public class PlayerStatus
    {
        public int Hp;
        public int MaxHp;
        public int Durability;
        public int MaxDurability;
        public int WalkSpeed;
        public int RunSpeed;
    }
    
    [Header("Player Settings")] 
    public Animator animator;

    public Transform leftHand;
    public Transform rightHand;

    protected virtual void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
    }
}
