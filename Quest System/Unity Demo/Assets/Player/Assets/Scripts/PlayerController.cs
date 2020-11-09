<<<<<<< HEAD
<<<<<<< HEAD
﻿using QSystem;
=======
﻿using QSystem;
>>>>>>> parent of 9c55c73... Latest
=======
﻿using QSystem;
>>>>>>> parent of 9c55c73... Latest
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float rotateSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    public QuestInventory QInventory;

    void Awake()
    {
        QInventory = new QuestInventory();
    }

<<<<<<< HEAD
<<<<<<< HEAD
    private void Start()
    {
        controller = GetComponent<CharacterController>();        
=======
    private void Start()
    {
        controller = GetComponent<CharacterController>();        
>>>>>>> parent of 9c55c73... Latest
=======
    private void Start()
    {
        controller = GetComponent<CharacterController>();        
>>>>>>> parent of 9c55c73... Latest
    }

    void Update()
    {
        if(controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if(Input.GetButtonDown("Fire1"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        else
        {
            moveDirection = new Vector3(0, moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }

        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
