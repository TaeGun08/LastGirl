using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HasParts
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
        public float DashForce;
        public float DashCooldown;
    }
    
    [Header("Player Settings")] 
    public PlayerController playerController;
    public Animator animator;
    
    public Transform leftHand;
    public Transform rightHand;

    public Transform aimPoint;

    public float headAngle;
    
    public bool IsShotReady { get; set; }
    public bool IsZoom { get; set; }
    public bool IsFire { get; set; }
    public bool IsReload { get; set; }
    public bool IsDead { get; set; }
    public bool UseDash { get; set; }
    public bool IsDashing { get; set; }
    public bool IsBarrierOn { get; set; }

    protected virtual void OnAnimatorIK(int layerIndex)
    {
        if (IsShotReady == false) return;

        Vector3 direction = (aimPoint.transform.position - transform.position).normalized;
        float target = Vector3.Angle(transform.forward, direction);
        
        if (target < headAngle * 0.5f)
        {
            animator.SetLookAtWeight(1.0f);
            animator.SetLookAtPosition(aimPoint.position);
        
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
        
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
        }
        
        if (IsReload) return;
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
    }
}
