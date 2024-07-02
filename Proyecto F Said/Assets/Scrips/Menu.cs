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
    [SerializeField] private GameObject Mousepuntero;
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
                Mousepuntero.SetActive(false);
                BotonDespausar.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                BotonPausa.SetActive(true);
                Mousepuntero.SetActive(true);
                BotonDespausar.SetActive(false);
            }
        }
    }//0(1) son operaciones constantes sin bucles 
    public void Pausar()
    {
        BotonPausa.SetActive(false);
        //Mousepuntero.SetActive(false);
        BotonDespausar.SetActive(true);
    }
    public void despausar()
    {
        BotonPausa.SetActive(true);
        //Mousepuntero.SetActive(true);
        BotonDespausar.SetActive(false);
    }
}
