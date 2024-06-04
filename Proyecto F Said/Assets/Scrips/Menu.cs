using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject BotonPausa;
    [SerializeField] private GameObject BotonDespausar;


    public void pausa()
    {
        Time.timeScale = 0f;
        BotonPausa.SetActive(false);
        BotonDespausar.SetActive(true);
    }

    public void reanudar()
    {
        Time.timeScale = 1f;
        BotonPausa.SetActive(true);
        BotonDespausar.SetActive(false);
    }
}
