using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatus
{
    IDLE,
    WALKING,
    CROUCHING,
    SPRINTING,
    SLIDING,
    VAULTING
};

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput input;

    [Header("Movement")]
    [SerializeField]
    [Tooltip("Determines how long it should take to transition from one speed to another")]
    private float accelerationTime = .15f;
    [SerializeField]
    [Tooltip("Maximum speed while walking")]
    private float walkSpeed = 5f;
    [SerializeField]
    [Tooltip("Maximum speed while sprinting")]
    private float sprintSpeed = 7f;
    [Header("Crouching and Sliding")]
    [SerializeField]
    [Tooltip("Maximum speed while crouching")]
    private float crouchSpeed = 2f;
    [SerializeField]
    [Tooltip("Initial speed when sliding")]
    private float initialSlideSpeed = 10f;
    [SerializeField]
    [Tooltip("Maximum speed that the player can slide at")]
    private float maxSlideSpeed = 12f;
    [SerializeField]
    [Tooltip("Amount of speed that should be lost per second while sliding")]
    private float slideDrag = 1f;
    [SerializeField]
    [Tooltip("Factor for how much slopes will increase/decrease speed while sliding (0 = Slopes shouldn't affect slide speed)")]
    private float slideSlopeSpeedModifier = 2f;
    private float Speed
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
                    return initialSlideSpeed;
                default:
                    return sprintSpeed;
            }
        }
    }

    [Header("Vertical Speed")]
    [SerializeField]
    [Tooltip("The height that the player should jump in Unity units (usually 1unit = 1meter)")]
    private float jumpHeight = 1f;
    [SerializeField]
    [Tooltip("The amount of gravity that the player should be experiencing")]
    private float gravity = -10f;
    [SerializeField]
    [Tooltip("Determines how far the player will be able to step down before being considered as falling")]
    private float groundDistance = 0.2f;
    [SerializeField]
    [Tooltip("Determines which layers the player will be able to stand on and be considered as grounded")]
    private LayerMask groundMask = ~0;
    private bool grounded;
    private Vector3 groundCheckSize;

    [Header("[Debug] (READ-ONLY)")]
    public PlayerStatus status = PlayerStatus.IDLE;
    public Vector3 desiredVelocity;
    public Vector3 velocity;
    private Vector3 acceleration;


    private float defaultHeight;
    private float defaultRadius;
    private float halfHeight;
    private float halfRadius;
    private Transform playerCamera;
    private Vector3 defaultCameraPosition;

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

        groundCheckSize = new Vector3(halfRadius, groundDistance, halfRadius);
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckCrouch();
        MoveDefault();
        Jump();
        ApplyDesiredVelocity();
    }


    /***************************************************/
    /******************** CROUCHING ********************/
    private void CheckCrouch()
    {
        if (input.Crouching && HorizontalMagnitude(velocity) <= walkSpeed)
        {
            Crouch();
        }
        else if (input.Crouch && HorizontalMagnitude(velocity) > walkSpeed)
        {
            velocity = HorizontalVector(velocity, out float vy);
            velocity = velocity.normalized * initialSlideSpeed;
            velocity.y = vy;
            Slide();
        }
        else if (status == PlayerStatus.SLIDING && grounded)
        {
            Slide();
        }
        else
        {
            Ray standUpCheck = new Ray(transform.position, transform.up);
            if (controller.height < 2f && Physics.Raycast(standUpCheck, halfHeight * 1.25f))
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
        controller.height = Mathf.Lerp(controller.height, halfHeight, .1f);
        playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, Vector3.zero, .1f);
        status = PlayerStatus.CROUCHING;
    }
    private void Uncrouch()
    {
        controller.height = Mathf.Lerp(controller.height, defaultHeight, .1f);
        playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, defaultCameraPosition, .1f);
        status = PlayerStatus.WALKING;
    }

    /*************************************************/
    /******************** JUMPING ********************/
    private void Jump()
    {
        Vector3 groundPosition = transform.position - new Vector3(0, controller.height / 2, 0);
        grounded = Physics.CheckBox(groundPosition, groundCheckSize, Quaternion.identity, groundMask);
        if (input.Jump && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    /*************************************************/
    /******************** SLIDING ********************/
    private void Slide()
    {
        Crouch();
        velocity -= HorizontalVector(velocity, out float originalY) * slideDrag * Time.deltaTime; // Subtract slide drag
        
        Ray downRay = new Ray(transform.position, -transform.up);
        Physics.Raycast(downRay, out RaycastHit hit, halfHeight, groundMask);
        velocity += HorizontalVector(velocity) * Vector3.Dot(transform.forward, hit.normal) * slideSlopeSpeedModifier * Time.deltaTime; // Add slope speed

        velocity.y = originalY;
        velocity = ClampHorizontalMagnitude(velocity, maxSlideSpeed);
        if (input.Jump)
        {
            status = PlayerStatus.CROUCHING;
        }
        else if (input.Sprint)
        {
            status = PlayerStatus.SPRINTING;
        }
        else if (HorizontalMagnitude(velocity) > crouchSpeed)
        {
            status = PlayerStatus.SLIDING;
        }
    }

    /************************************************/
    /******************** MOVING ********************/
    private void MoveDefault()
    {
        if ((int)status <= 3)
        {
            Vector2 movementInput = input.MovementInput;
            desiredVelocity = transform.forward * movementInput.y + transform.right * movementInput.x;

            if (input.Sprinting && desiredVelocity != Vector3.zero)
            {
                status = PlayerStatus.SPRINTING;
            }
            else if (input.Crouching || controller.height < defaultHeight - .5f)
            {
                status = PlayerStatus.CROUCHING;
            }
            else if (desiredVelocity != Vector3.zero)
            {
                status = PlayerStatus.WALKING;
            }
            else
            {
                status = PlayerStatus.IDLE;
            }
        }

    }
    private void ApplyDesiredVelocity()
    {
        desiredVelocity *= Speed;

        if (status != PlayerStatus.SLIDING)
            velocity = Vector3.SmoothDamp(velocity, new Vector3(desiredVelocity.x, velocity.y, desiredVelocity.z), ref acceleration, accelerationTime);

        if (grounded && velocity.y < 0)
        {
            velocity.y = gravity/2;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
        
    }

    /*************************************************/
    /******************** HELPERS ********************/
    private Vector3 HorizontalVector(Vector3 input)
    {
        input.y = 0;
        return input;
    }
    private Vector3 HorizontalVector(Vector3 input, out float y)
    {
        y = input.y;
        return HorizontalVector(input);
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
