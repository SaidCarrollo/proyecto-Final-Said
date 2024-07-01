using System.Collections;using System.Collections.Generic;
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
    public TextMeshProUGUI playerControlsText;
    public TextMeshProUGUI ObjetiveText;
    public TextMeshProUGUI adviceText;
    [SerializeField] private float rotationSpeed = 5f;
    public AudioSource Tomar;
    public AudioSource Caminar;
    public AudioSource Correr;
    void Start()
    {
        originalVelocity = velocity;
        infoText.alpha = 0f;
        Time.timeScale = 1f;
        ShowcontrolsText("Controles:\n" +
                                 "Movimiento: WASD " +
                                 "Correr: Shift\n" +
                                 "Agarrar: E\n"+
                                 "Camara: Mouse");
        ShowObjetiveText("Busca una salida");
        AdviceText("Las puertas se pueden traspasar");

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
        if (context.performed)
        {
                Caminar.Play();
        }
        else if (context.canceled)
        {
            Caminar.Stop();
        }
    }
    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            velocity = runVelocity;
            Caminar.Stop();
            if (!Correr.isPlaying)
            {
                Correr.Play();
            }
        }
        else if (context.canceled)
        {
            velocity = originalVelocity;
            Correr.Stop();
            if (_movement != Vector2.zero && !Caminar.isPlaying)
            {
                Caminar.Play();
            }
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
    private void ShowText(TextMeshProUGUI textComponent, string message, float displayDuration)
    {
        float fadeDuration = 0.5f;
        textComponent.text = message;
        textComponent.alpha = 0f;
        textComponent.DOFade(1f, fadeDuration);
        DOVirtual.DelayedCall(displayDuration, () =>
        {
            textComponent.DOFade(0f, fadeDuration);
        });
    }
    private void ShowcontrolsText(string message)
    {
        ShowText(playerControlsText, message, 5.0f);
    }

    private void ShowObjetiveText(string message)
    {
        ShowText(ObjetiveText, message, 5.0f);
    }

    private void AdviceText(string message)
    {
        ShowText(adviceText, message, 10.0f);
    }

    private void ShowInfoText(string message)
    {
        ShowText(infoText, message, 1.0f);
    }
}
