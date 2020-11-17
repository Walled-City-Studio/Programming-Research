﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField]
        private float mouseSensitivity = 100f;
        [SerializeField]
        private float aspectRatio = 16 / 9;

        private Transform playerBody;
        private float xRotation = 0f;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerBody = transform.parent;
        }

        // Update is called once per frame
        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            xRotation -= mouseY * mouseSensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX * mouseSensitivity * aspectRatio * Time.deltaTime);

        }
    }

}