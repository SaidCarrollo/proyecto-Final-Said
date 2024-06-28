using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using TMPro;
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
    private Camera mainCamera;
    public GameObject ListadeObjetos;
    public TextMeshProUGUI infoText;
    [SerializeField] private float rotationSpeed = 5f;
    public AudioSource Tomar; 

    void Start()
    {
        originalVelocity = velocity;
        infoText.alpha = 0f;
       /*   Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;*/
    }
  
   /* private void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }*/
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
            Vector3 moveDirection = (forward * _movement.y + right * _movement.x).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
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
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, raycastDistance, interactableLayer))
            {
                GameObject objectToDisable = hitInfo.collider.gameObject;

                if (objectToDisable.CompareTag("Objeto"))
                {
                    Tomar.Play();
                    AnimateAndDestroy(objectToDisable);
                }
                else
                {
                    Debug.LogWarning("El objeto no tiene el tag 'Objeto'. No se puede agarrar.");
                    ShowInfoText("No se puede agarrar este objeto");
                }
            }
        }
    }

    private void AnimateAndDestroy(GameObject objectToDisable)
    {
        float duration = 1.0f;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(objectToDisable.transform.DOScale(Vector3.zero, duration));
        sequence.Join(objectToDisable.transform.DORotate(new Vector3(0, 360, 0), duration, RotateMode.FastBeyond360));

        sequence.OnComplete(() =>
        {
            Item item = objectToDisable.GetComponent<Item>();
            if (item != null)
            {
                string itemName = item.itemName;
                if (ListadeObjetos != null)
                {
                    ListadeObjetos.GetComponent<ListadeObjetos>().UpdateInventoryText(itemName);
                }
                else
                {
                    Debug.LogWarning("El GameObject ListadeObjetos no está asignado en el Inspector.");
                }
            }
            else
            {
                Debug.LogWarning("El objeto no tiene el script Item.");
            }
            objectToDisable.SetActive(false);
        });
    }
    private void ShowInfoText(string message)
    {
        float duration = 0.5f;
        infoText.text = message;
        infoText.alpha = 0f;
        infoText.DOFade(1f, duration);
        DOVirtual.DelayedCall(1.0f, () =>
        {
            infoText.DOFade(0f, duration);
        });
    }
  
}
