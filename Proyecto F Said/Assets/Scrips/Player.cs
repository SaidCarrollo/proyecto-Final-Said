using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Rigidbody myRB;
    [SerializeField] public float velocity;
    [SerializeField] private float runVelocity = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private AnimationCurve accelerationCurve;
    private float originalVelocity;
    private float timeSinceStartedRunning = 0f;
    public float raycastDistance = 19f;
    public LayerMask interactableLayer;
    private PriorityQueue<GameObject> inventory;
    private Camera mainCamera;
    public Gamemanager gamemanger;
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
        if (_movement != Vector2.zero)
        {
            timeSinceStartedRunning += Time.fixedDeltaTime;
        }
        else
        {
            timeSinceStartedRunning = 0f;
        }
        float currentAcceleration = accelerationCurve.Evaluate(timeSinceStartedRunning);
        float currentVelocity = velocity * currentAcceleration;
        Vector3 desiredMoveDirection = (forward * _movement.y + right * _movement.x).normalized * currentVelocity;
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
    public void Grab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, raycastDistance, interactableLayer))
            {
                GameObject objectToDestroy = hitInfo.collider.gameObject;
                AnimateAndDestroy(objectToDestroy);
            }
        }
    }

    private void AnimateAndDestroy(GameObject objectToDestroy)
    {
        float duration = 1.0f;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(objectToDestroy.transform.DOScale(Vector3.zero, duration));
        sequence.Join(objectToDestroy.transform.DORotate(new Vector3(0, 360, 0), duration, RotateMode.FastBeyond360));

        sequence.OnComplete(() =>
        {
            Item item = objectToDestroy.GetComponent<Item>();
            if (item != null)
            {
                string itemName = item.itemName;
                if (gamemanger != null)
                {
                    gamemanger.UpdateInventoryText(itemName);
                }
                else
                {
                    Debug.LogWarning("GameManager no est� asignado en Player script.");
                }
            }
            else
            {
                Debug.LogWarning("El objeto no tiene el script Item.");
            }

            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
            }
        });
    }
}
