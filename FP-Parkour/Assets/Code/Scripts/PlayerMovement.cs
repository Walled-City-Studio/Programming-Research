using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public enum PlayerStatus { IDLE, WALKING, CROUCHING, SPRINTING, SLIDING, VAULTING };

public class PlayerMovement : MonoBehaviour
{
    public PlayerStatus status = PlayerStatus.IDLE;
    
    private CharacterController controller;
    private PlayerInput input;

    [Header("Movement Speed")]
    [SerializeField] [Tooltip("Maximum speed while walking")]
    private float walkSpeed = 4f;
    [SerializeField] [Tooltip("Maximum speed while sprinting")]
    private float sprintSpeed = 8f;
    [SerializeField] [Tooltip("Maximum speed while crouching")]
    private float crouchSpeed = 1.5f;
    [SerializeField] [Tooltip("Maximum speed while sliding")]
    private float slideSpeed = 10f;
    [SerializeField] [Tooltip("Determines how fast the player should accelerate")]
    private float accelerationFactor = 50f;
    private float MaxSpeed
    {
        get
        {
            switch (status)
            {
                case PlayerStatus.WALKING:
                    return walkSpeed;
                case PlayerStatus.SPRINTING:
                    return sprintSpeed;
                case PlayerStatus.CROUCHING:
                    return crouchSpeed;
                case PlayerStatus.SLIDING:
                    return slideSpeed;
                default:
                    return sprintSpeed;
            }
        }
    }

    [Header("Vertical Speed")]
    [SerializeField] [Tooltip("The height that the player should jump in Unity units (usually 1unit = 1meter)")]
    private float jumpHeight = 1f;
    [SerializeField] [Tooltip("The amount of gravity that the player should be experiencing.")]
    private float gravity = -9.81f;

    public Vector3 acceleration;
    public Vector3 velocity;

    private float defaultHeight;
    private float defaultRadius;
    private float halfHeight;
    private float halfRadius;
    private Transform playerCamera;
    private Vector3 defaultCameraPosition;

    private bool grounded;
    private Transform groundCheck;
    private float groundDistance = 0.2f;
    [SerializeField]
    private LayerMask groundMask = ~0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        playerCamera = transform.Find("FPS Camera");
        defaultCameraPosition = playerCamera.localPosition;
        defaultHeight = controller.height;
        defaultRadius = controller.radius;
        halfHeight = defaultHeight / 2;
        halfRadius = defaultRadius / 2;

        groundCheck = transform.Find("Ground Check");
    }

    // Update is called once per frame
    void Update()
    {
        CheckCrouch();
        Move();
        GroundCheck();
        Jump();
        CalculateVelocity();
    }

    private void Move()
    {
        if ((int)status <= 3)
        {
            Vector2 movementInput = input.MovementInput;
            Vector3 movementAcceleration = transform.forward * movementInput.y + transform.right * movementInput.x;
            acceleration = movementAcceleration * accelerationFactor;
            if (input.Sprinting)
            {
                status = PlayerStatus.SPRINTING;
            }
            else if (input.Crouching)
            {
                status = PlayerStatus.CROUCHING;
            }
            else
            {
                status = PlayerStatus.WALKING;
            }
        }
        
    }

    /******************** CROUCHING ********************/
    private void CheckCrouch()
    {
        if (input.Crouching && ! input.Sprinting)
        {
            Crouch();
        }
        else
        {
            Ray crouchRay = new Ray(transform.position, transform.up);
            bool cantStandUp = (controller.height < 2f && Physics.Raycast(crouchRay, 1.25f));
            if (cantStandUp)
            {
                Crouch();
            }
            else
            {
                Uncrouch();
            }
        }
    }
    private void Crouch()
    {
        controller.height = Mathf.Lerp(controller.height, halfHeight, 0.25f);
        playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, Vector3.zero, 0.25f);
        status = PlayerStatus.CROUCHING;
    }
    private void Uncrouch()
    {
        controller.height = Mathf.Lerp(controller.height, defaultHeight, 0.25f);
        playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, defaultCameraPosition, 0.25f);
        status = PlayerStatus.WALKING;
    }

    /******************** JUMPING ********************/
    private void Jump()
    {
        if (input.Jump && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    private void GroundCheck()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }


    private void CalculateVelocity()
    {
        acceleration.y = gravity;

        velocity += acceleration * Time.deltaTime;

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity = ClampHorizontalMagnitude(velocity, MaxSpeed);
        controller.Move(velocity * Time.deltaTime);
        
    }

    private float HorizontalMagnitude(Vector3 input)
    {
        input.y = 0;
        return input.magnitude;
    }
    private Vector3 ClampHorizontalMagnitude(Vector3 input, float maxLength)
    {
        Vector3 accel = new Vector3(input.x, 0, input.z);
        accel = Vector3.ClampMagnitude(accel, maxLength);
        accel.y = input.y;
        return accel;
    }
}
