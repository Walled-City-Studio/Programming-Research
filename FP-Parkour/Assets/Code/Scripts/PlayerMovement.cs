using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 acceleration;
    private Vector3 velocity;
    private CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = transform.Find("FPS Camera");
        CameraHeight = cameraHeight = playerCamera.localPosition.y;
        desiredHeight = controller.height;
        groundCheck = transform.Find("Ground Check");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Crouch();
        Jump();
    }



    [Header("Speed")]

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float sprintSpeedModifier = 1.5f;
    public float Speed
    {
        get
        {
            float realSpeed = speed;
            realSpeed *= sprinting ? sprintSpeedModifier : 1;
            realSpeed *= crouching ? crouchSpeedModifier : 1;
            return realSpeed;
        }
    }
    [HideInInspector]
    public bool sprinting;

    private void Move()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        sprinting = Input.GetAxisRaw("Sprint") != 0;

        Vector3 movement = transform.right * xMovement + transform.forward * zMovement;

        controller.Move(movement * Speed * Time.deltaTime);
    }



    [Header("Crouching")]

    [SerializeField]
    private float crouchSpeedModifier = 0.5f;
    [SerializeField]
    private float cameraHeight = 0.75f;
    private float CameraHeight { get; set; }
    private float desiredHeight = 2f;
    private Transform playerCamera;
    [HideInInspector]
    public bool crouching;
    private void Crouch()
    {
        Ray crouchRay = new Ray(transform.position, transform.up);
        crouching = (Input.GetAxisRaw("Crouch") != 0) || (controller.height < 2f && Physics.Raycast(crouchRay, 1.25f));

        if (crouching)
        {
            CameraHeight = 0f;
            desiredHeight = 1f;
        }
        else
        {
            CameraHeight = cameraHeight;
            desiredHeight = 2f;
        }

        float newHeight = Mathf.Lerp(controller.height, desiredHeight, .1f);
        controller.height = newHeight;

        Vector3 newCameraPosition = Vector3.Lerp(playerCamera.transform.localPosition, new Vector3(0, CameraHeight, 0), 0.1f);
        playerCamera.transform.localPosition = newCameraPosition;

    }



    [Header("Jumping & Gravity")]

    [SerializeField]
    private float jumpHeight = 1f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float groundDistance = 0.1f;
    [SerializeField]
    private LayerMask groundMask = new LayerMask();
    private Transform groundCheck;
    [HideInInspector]
    public bool isGrounded;
    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CalculateVelocity()
    {

    }
}
