using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject BotonPausa;
    [SerializeField] private GameObject BotonDespausar;
    public Volume GlobalVolume;
    public Vignette viñeta;
    private bool estaEnPausa = false;
    private void Awake()
    {
        viñeta = (Vignette)GlobalVolume.profile.components.Find(c => c is Vignette);
    }
    public void Pausa(InputAction.CallbackContext contexto)
    {
        if (contexto.performed)
        {
            estaEnPausa = !estaEnPausa;

            if (estaEnPausa)
            {
                Time.timeScale = 0f;
                BotonPausa.SetActive(false);
                BotonDespausar.SetActive(true);
                viñeta.intensity.value = 0.843f;
            }
            else
            {
                Time.timeScale = 1f;
                BotonPausa.SetActive(true);
                BotonDespausar.SetActive(false);
                viñeta.intensity.value = 0f;
            }
        }
    }
}
