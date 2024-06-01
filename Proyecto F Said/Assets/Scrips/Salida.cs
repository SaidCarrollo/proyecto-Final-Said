using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida : MonoBehaviour
{
    public Player play;
    public GameObject BADE;
    public GameObject GodE;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Puerta" && play.velocity ==5 )
        {
            BADE.SetActive(true);
            Time.timeScale = 0;
        }
        if (collision.gameObject.tag == "Puerta" && play.velocity != 5)
        {
            GodE.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
