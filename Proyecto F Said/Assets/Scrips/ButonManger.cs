using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButonManger : MonoBehaviour
{
    public GameObject Abrir;
    public GameObject Cerrar;
    public void AbrirLista()
    {
        Abrir.SetActive(false);
        Cerrar.SetActive(true);
    }
    public void CerrarLista()
    {
        Abrir.SetActive(true);
        Cerrar.SetActive(false);
    }

}
