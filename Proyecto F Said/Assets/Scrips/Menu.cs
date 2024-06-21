using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject BotonPausa;
    [SerializeField] private GameObject BotonDespausar;

    private bool estaEnPausa = false;
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
            }
            else
            {
                Time.timeScale = 1f;
                BotonPausa.SetActive(true);
                BotonDespausar.SetActive(false);
            }
        }
    }
}
