using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;              // Reference to the Player
    public Vector3 offset = new Vector3(0, 2, -5); // Offset behind the character
    public float rotationSpeed = 2f;      // Speed of camera rotation
    public float pitchMin = -30f;         // Minimum pitch angle
    public float pitchMax = 60f;          // Maximum pitch angle
    public Transform targetLookAtOffset;  // Optional: Empty GameObject set higher for the camera's target
    public float smoothTime = 0.1f;       // Smoothness factor for camera movement

    public Animator characterAnimator;
    public LayerMask obstacleLayers;      // Layer mask for obstacles

    private float currentYaw = 0f;
    private float currentPitch = 20f;     // Starting pitch value
    private Vector3 smoothVelocity;
    private float currentDistance;

    private Vector3 cameraForward; // Backing field for CameraForward
    public Vector3 CameraForward => cameraForward; // Public read-only property

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
        Cursor.visible = false;                   // Hide cursor during gameplay

        if (characterAnimator == null)
        {
            Debug.LogError("Animator component not found on the target character.");
        }

        currentDistance = offset.magnitude; // Initial distance based on offset magnitude
    }

    void LateUpdate()
    {
        if (target == null || characterAnimator == null) return;

        bool isAiming = Input.GetMouseButton(1);

        FollowTargetWithInput();

        if (isAiming)
        {
            UpdateCameraForward(); // Update forward direction for character aiming
        }
    }

    private void FollowTargetWithInput()
    {
        currentYaw += Input.GetAxis("Mouse X") * rotationSpeed;
        currentPitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentPitch = Mathf.Clamp(currentPitch, pitchMin, pitchMax);

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 desiredPosition = target.position - rotation * offset;

        // Adjust position for obstacles
        desiredPosition = AdjustCameraForObstacles(desiredPosition);

        // Smoothly move the camera
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref smoothVelocity, smoothTime);

        if (targetLookAtOffset != null)
        {
            transform.LookAt(targetLookAtOffset.position);
        }
        else
        {
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }
    }

    private void UpdateCameraForward()
    {
        cameraForward = transform.forward;
        cameraForward.y = 0; // Keep the forward vector flat
        cameraForward.Normalize(); // Normalize for consistency
    }

    private Vector3 AdjustCameraForObstacles(Vector3 desiredPosition)
    {
        RaycastHit hit;
        Vector3 direction = desiredPosition - target.position;

        if (Physics.Raycast(target.position, direction.normalized, out hit, currentDistance, obstacleLayers))
        {
            float hitDistance = hit.distance - 0.5f; // Small buffer
            return target.position + direction.normalized * Mathf.Clamp(hitDistance, 1f, currentDistance);
        }

        return desiredPosition;
    }
}