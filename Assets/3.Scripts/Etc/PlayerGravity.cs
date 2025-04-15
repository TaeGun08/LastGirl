using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : Gravity
{
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateGravity();
    }

    public override void UpdateGravity()
    {
        if (characterController.isGrounded)
        {
            if (velocity != 0)
            {
                velocity = 0f;
            }
            return;
        }
        
        velocity -= gravity * Time.deltaTime;
        Vector3 direction = new Vector3(0, velocity, 0);
        characterController.Move(direction * Time.deltaTime); 
    }
}
