using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida : MonoBehaviour
{
    public Player play;
    public GameObject BADE;
    public GameObject GodE;
    private Tree<string> endingsTree;
    public GameObject[] finalScreens;
    private SimplyLinkedList<GameObject> finalScreenList; // Lista para pantallas finales

    private void Start()
    {
        InitializeEndingsTree();
        InitializeFinalScreenList();
    }

    private void InitializeEndingsTree()
    {
 
        endingsTree = new Tree<string>();
        endingsTree.AddNode("Final Normal", "root");
        endingsTree.AddNode("Final Secreto", "Final Normal");
        endingsTree.AddNode("Final R�pido", "Final Secreto");

    }

    private void InitializeFinalScreenList()
    {
        finalScreenList = new SimplyLinkedList<GameObject>();

        finalScreenList.InsertNodeAtEnd(finalScreens[0]); 
        finalScreenList.InsertNodeAtEnd(finalScreens[1]); 
        finalScreenList.InsertNodeAtEnd(finalScreens[2]); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Puerta")
        {
            CheckEndCondition();
        }
    }

    private void CheckEndCondition()
    {
        string condition = DetermineCondition();
        Tree<string>.TreeNode rootNode = endingsTree.GetRoot();
        string finalName = endingsTree.FindFinal(rootNode, condition);

        ShowFinalScreen(finalName);
    }

    private string DetermineCondition()
    {
        // Aqu� determina la condici�n basada en la velocidad del jugador, objetos recogidos, etc.
        // Devuelve un string que corresponda a una condici�n en el �rbol de finales
        if (play.velocity > 5)
        {
            return "Final R�pido";
        }
        else if (play.velocity<=5)
        {
            return "Final Secreto";
        }
        else
        {
            return "Final Normal";
        }
    }

    private void ShowFinalScreen(string finalName)
    {
        GameObject finalScreen = null;

        // Obtener la pantalla correspondiente seg�n el nombre del final
        if (finalName == "Final Normal")
        {
            finalScreen = finalScreenList.ObtainNodeAtPosition(0);
        }
        else if (finalName == "Final Secreto")
        {
            finalScreen = finalScreenList.ObtainNodeAtPosition(1);
        }
        else if (finalName == "Final R�pido")
        {
            finalScreen = finalScreenList.ObtainNodeAtPosition(2);
        }

        if (finalScreen != null)
        {
            finalScreen.SetActive(true);
            Time.timeScale = 0; 
        }
        else
        {
            Debug.LogWarning("No se encontr� una pantalla para el final: " + finalName);
        }
    }
}
