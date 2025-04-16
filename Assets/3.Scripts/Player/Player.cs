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

    public Transform aimPoint;
    
    public bool IsShotReady { get; set; }
    public bool IsZoom { get; set; }
    public bool IsFire { get; set; }

    protected virtual void OnAnimatorIK(int layerIndex)
    {
        if (IsShotReady == false) return;
        
        animator.SetLookAtWeight(1.0f);
        animator.SetLookAtPosition(aimPoint.position);
        
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
        
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
    }
}
