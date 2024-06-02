using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Rigidbody myRB;
    [SerializeField] public float velocity;
    [SerializeField] private float runVelocity = 5f;
    [SerializeField] private Transform cameraTransform;
    private float originalVelocity;

    void Start()
    {
        originalVelocity = velocity;
    }

    void FixedUpdate()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * _movement.y + right * _movement.x).normalized * velocity;
        myRB.velocity = new Vector3(desiredMoveDirection.x, myRB.velocity.y, desiredMoveDirection.z);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>() * velocity;
    }
    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            velocity = runVelocity;
        }
        else if (context.canceled)
        {
            velocity = originalVelocity;
        }
        _movement = _movement.normalized * velocity;
    }
}
