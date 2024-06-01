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

    private float originalVelocity;

    void Start()
    {
        originalVelocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        myRB.velocity = new Vector3(_movement.x, myRB.velocity.y, _movement.y);
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
