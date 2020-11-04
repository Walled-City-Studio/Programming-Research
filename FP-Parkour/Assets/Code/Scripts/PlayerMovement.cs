using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PlayerMovement : MonoBehaviour
{

    [Header("Speed")]

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float sprintModifer = 1.5f;
    [HideInInspector]
    public bool sprinting;
    public float Speed
    {
        get
        {
            float realSpeed = speed;
            realSpeed *= sprinting ? sprintModifer : 1;
            realSpeed *= crouchScript.crouching ? crouchScript.crouchSpeedModifier : 1;
            return realSpeed;
        }
    }

    private CharacterController controller;
    private PlayerCrouch crouchScript;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        crouchScript = GetComponent<PlayerCrouch>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        sprinting = Input.GetAxisRaw("Sprint") != 0;

        Vector3 movement = transform.right * xMovement + transform.forward * zMovement;

        controller.Move(movement * Speed * Time.deltaTime);
    }
}
