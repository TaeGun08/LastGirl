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

    public bool isShot;
    
    protected virtual void OnAnimatorIK(int layerIndex)
    {
        if (isShot == false) return;
        
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
        
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
    }
}
