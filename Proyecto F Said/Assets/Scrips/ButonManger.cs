using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButonManger : MonoBehaviour
{
    public GameObject Abrir;
    public GameObject Cerrar;
    public GameObject izquierda;
    public GameObject Derecha;
    public AudioSource Comienzo;
    public ButomManager butomManager;
    public GameObject Creditos;
    public void AbrirLista()
    {
        Abrir.SetActive(true);
        Cerrar.SetActive(true);
        Derecha.SetActive(true);
        izquierda.SetActive(true);
    }
    public void CerrarLista()
    {
        Abrir.SetActive(false);
        Cerrar.SetActive(false);
        Derecha.SetActive(false);
        izquierda.SetActive(false);
    }
    public void CargarGameplayConRetardo()
    {
        StartCoroutine(CargarConRetardo());
        SceneManager.LoadScene("Gameplay");
    }

    private IEnumerator CargarConRetardo()
    {
        yield return new WaitForSeconds(3.0f);
        Comienzo.Play();
        SceneManager.LoadScene("Gameplay");
    }
    public void Regresar()
    {
        SceneManager.LoadScene("Menu");
    }
    public void IniciarTemblorDeCamara()
    {
        butomManager.StartCameraShake(1.0f, 2.0f, 3.0f);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public void OpenCredit()
    {
        Creditos.SetActive(true);
    }
    public void CloseCredit()
    {
        Creditos.SetActive(false);
    }
}