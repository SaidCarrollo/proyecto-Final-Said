using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida : MonoBehaviour
{
    public Player play;
    public Gamemanager gamemanger;
    private Tree<string> endingsTree;
    public GameObject[] finalScreens;
    private Queue<GameObject> finalScreenQueue;

    private void Start()
    {
        InitializeEndingsTree();
        InitializeFinalScreenQueue();
    }

    private void InitializeEndingsTree()
    {
        endingsTree = new Tree<string>();
        endingsTree.AddNode("Final Normal", "root");
        endingsTree.AddNode("Final Secreto", "Final Normal");
        endingsTree.AddNode("Final Rápido", "Final Secreto");
        endingsTree.AddNode("Final Celular", "Final Normal");
    }

    private void InitializeFinalScreenQueue()
    {
        finalScreenQueue = new Queue<GameObject>();

        finalScreenQueue.Enqueue(finalScreens[0]);
        finalScreenQueue.Enqueue(finalScreens[1]);
        finalScreenQueue.Enqueue(finalScreens[2]);
        finalScreenQueue.Enqueue(finalScreens[3]);
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
            GameObject firstItem = gamemanger.GetFirstItem();
            if (firstItem != null && firstItem.name == "Celular")
            {
                Debug.Log("Returning 'Final Celular'");
                return "Final Celular";
            }
            else
            {
                Debug.Log("Returning 'Final Normal'");
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
            finalScreen = finalScreenQueue.Dequeue();
        }
        else if (finalName == "Final Secreto")
        {
            finalScreen = finalScreenQueue.Dequeue();
        }
        else if (finalName == "Final Rápido")
        {
            finalScreen = finalScreenQueue.Dequeue();
        }
        else if (finalName == "Final Celular")
        {
            finalScreen = finalScreenQueue.Dequeue();
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
