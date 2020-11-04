using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [Header("Crouching")]
    public float crouchSpeedModifier = 0.5f;
    [SerializeField]
    private float cameraHeight = 0.75f;

    private float CameraHeight { get; set; }
    private float desiredHeight = 2f;

    private CharacterController controller;
    private Transform playerCamera;
    [HideInInspector]
    public bool crouching;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = transform.Find("FPS Camera");
        CameraHeight = cameraHeight = playerCamera.localPosition.y;
        desiredHeight = controller.height;
    }

    // Update is called once per frame
    void Update()
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
}
