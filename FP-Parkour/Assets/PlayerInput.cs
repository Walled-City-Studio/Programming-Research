﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MovementInput
    {
        get
        {
            float sidewaysMovement = Input.GetAxis("Horizontal");
            float forwardMovement = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(sidewaysMovement, forwardMovement);
            Debug.Log("PlayerInput.MovementInput before clamping: " + movement);
            return Vector2.ClampMagnitude(movement, 1);
        }
    }

    public Vector2 RawMovementInput
    {
        get
        {
            float sidewaysMovement = Input.GetAxis("Horizontal");
            float forwardMovement = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(sidewaysMovement, forwardMovement);
            return movement.normalized;
        }

    }

    public bool Sprinting
    {
        get { return Input.GetButton("Sprint"); }
    }

    public bool Crouch
    {
        get { return Input.GetButtonDown("Crouch"); }
    }

    public bool Crouching
    {
        get { return Input.GetButton("Crouch"); }
    }

    public bool Jump
    {
        get { return Input.GetButtonDown("Jump"); }
    }
}