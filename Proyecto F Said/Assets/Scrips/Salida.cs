using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida : MonoBehaviour
{
    public Player play;
    public Gamemanager gamemanger;
    public GameObject BADE;
    public GameObject GodE;
    private Tree<string> endingsTree;
    public GameObject[] finalScreens;
    private SimplyLinkedList<GameObject> finalScreenList; 

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
        endingsTree.AddNode("Final Rápido", "Final Secreto");
        endingsTree.AddNode("Final Celular", "Final Normal");
    }

    private void InitializeFinalScreenList()
    {
        finalScreenList = new SimplyLinkedList<GameObject>();

        finalScreenList.InsertNodeAtEnd(finalScreens[0]); 
        finalScreenList.InsertNodeAtEnd(finalScreens[1]); 
        finalScreenList.InsertNodeAtEnd(finalScreens[2]);
        finalScreenList.InsertNodeAtEnd(finalScreens[3]);
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

        if (play.velocity == 5)
        {
            return "Final Rápido";
        }
        else if (play.velocity <= 3)
        {
            GameObject firstItem = gamemanger.exampleList.ObtainNodeAtPosition(0);
            if (firstItem != null && firstItem.name == "Celular")
            {
                return "Final Celular"; 
            }
            else
            {
                return "Final Normal";
            }
        }
        else
        {
            return "Final Rapido";
        }
    }

    private void ShowFinalScreen(string finalName)
    {
        GameObject finalScreen = null;

        if (finalName == "Final Normal")
        {
            finalScreen = finalScreenList.ObtainNodeAtPosition(0);
        }
        else if (finalName == "Final Secreto")
        {
            finalScreen = finalScreenList.ObtainNodeAtPosition(1);
        }
        else if (finalName == "Final Rápido")
        {
            finalScreen = finalScreenList.ObtainNodeAtPosition(2);
        }
        else if (finalName == "Final Celular")
        {
            finalScreen = finalScreenList.ObtainNodeAtPosition(3);
        }
        if (finalScreen != null)
        {
            finalScreen.SetActive(true);
            Time.timeScale = 0; 
        }
        else
        {
            Debug.LogWarning("No se encontró una pantalla para el final: " + finalName);
        }
    }
}
