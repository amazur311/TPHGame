using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public Transform cameraTransform;
    public float rotationSpeed = 10f; // Speed for smooth rotation
    public CameraController cameraController; // Reference to the CameraController script

    private CharacterController characterController;
    private Animator characterAnimator;
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();

        if (cameraController == null)
        {
            Debug.LogError("CameraController reference not set in the inspector.");
        }
    }

    private void Update()
    {
        // Ground check
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep character grounded
        }

        // Check if right mouse button is held
        bool isAiming = Input.GetMouseButton(1);

        // Handle movement only if not aiming
        if (!isAiming)
        {
            HandleMovement();
        }

        // Rotate character along with the camera when aiming
        if (isAiming)
        {
            HandleAimingRotation(cameraController.CameraForward);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Handle jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (characterAnimator.GetBool("Grappled") == true)
        {
            moveSpeed = 0;
        }
        else
            moveSpeed = 2.0f;
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movement direction based on camera
        Vector3 move = cameraTransform.right * horizontal + cameraTransform.forward * vertical;
        move.y = 0;

        if (move.magnitude >= 0.1f)
        {
            // Normalize movement and apply speed
            move = move.normalized * moveSpeed;

            // Rotate character to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the character
        characterController.Move(move * Time.deltaTime);
    }

    private void HandleAimingRotation(Vector3 cameraForward)
    {
        if (cameraForward.magnitude >= 0.1f)
        {
            // Rotate character to face camera forward direction
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}