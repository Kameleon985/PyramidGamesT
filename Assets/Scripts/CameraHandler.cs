using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraHandler : MonoBehaviour
{
    private CameraControls cameraControls;
    private InputAction movement;
    private Transform cameraTransform;
    private InputAction rotation;


    [SerializeField]
    private float maxSpeed = 5f;
    private float moveSpeed;
    [SerializeField]
    private float acceleration = 10f;
    [SerializeField]
    private float damping = 15f;
    [SerializeField]
    private float maxRotationSpeed = 1f;

    private Vector3 targetPosition;
    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;


    private void Awake()
    {
        cameraControls = new CameraControls();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        lastPosition = transform.position;
        movement = cameraControls.Camera.Movement;
        rotation = cameraControls.Camera.RotateCamera;
        cameraControls.Camera.Enable();
    }

    private void Update()
    {
        GetKeyboardMovement();
        UpdateVelocity();
        UpdatePosition();
        RotateCamera();
    }

    private void OnDisable()
    {
        cameraControls.Disable();
    }

    private void UpdateVelocity()
    {
        horizontalVelocity = (transform.position - lastPosition) / Time.deltaTime;
        horizontalVelocity.y = 0f;
        lastPosition = transform.position;
    }

    private void RotateCamera()
    {
        float axisRotation = rotation.ReadValue<float>();
        transform.rotation = Quaternion.Euler(0f, axisRotation * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
    }

    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight() + movement.ReadValue<Vector2>().y * GetCameraForward();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
        {
            targetPosition += inputValue;
        }
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        return forward;
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0;
        return right;
    }

    private void UpdatePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * moveSpeed * Time.deltaTime;
        }
        else
        {
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }

        targetPosition = Vector3.zero;
    }
}
