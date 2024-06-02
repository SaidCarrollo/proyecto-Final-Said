using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButomManager : MonoBehaviour
{
    [SerializeField] private GameObject Bad1;
    [SerializeField] private GameObject God1;
    public string SceneName;
    public void Cerrar()
    {
        Bad1.SetActive(false);
    }
    public void Cerrar2()
    {
        God1.SetActive(false);
    }
    public void Gameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }


}
