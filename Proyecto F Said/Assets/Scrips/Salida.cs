using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Salida : MonoBehaviour
{
    public Player play;
    public GameObject Listadeobjetos;
    private Tree<string> endingsTree;
    public GameObject[] finalScreens;
    private SimplyLinkedList<GameObject> finalScreenList;
    public float tiempoMaximo = 30f; 
    private float tiempoRestante;
    private bool isTimeUp = false;
    public TextMeshProUGUI textoTiempo;
    private void Start()
    {
        InitializeEndingsTree();
        InitializeFinalScreenList();
        tiempoRestante = tiempoMaximo;
    }
    private void Update()
    {
        if (!isTimeUp)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                isTimeUp = true;
                HandleTimeUp(); 
            }

            UpdateTimeText();
        }
    }
    private void InitializeEndingsTree()
    {
        endingsTree = new Tree<string>();
        endingsTree.AddNode("Final Normal", "root");
        endingsTree.AddNode("Final Secreto", "root");
        endingsTree.AddNode("Final Rápido", "Final Secreto");
        endingsTree.AddNode("Final Celular", "Final Normal");
        endingsTree.AddNode("Final Mochila", "Final Normal");
        endingsTree.AddNode("Final Ascensor", "root");
        endingsTree.AddNode("Final Tiempo", "root");
    }

    private void InitializeFinalScreenList()
    {
        finalScreenList = new SimplyLinkedList<GameObject>();

        finalScreenList.InsertNodeAtEnd(finalScreens[0]); // Final Normal
        finalScreenList.InsertNodeAtEnd(finalScreens[1]); // Final Secreto
        finalScreenList.InsertNodeAtEnd(finalScreens[2]); // Final Rápido
        finalScreenList.InsertNodeAtEnd(finalScreens[3]); // Final Celular
        finalScreenList.InsertNodeAtEnd(finalScreens[4]); // Final Mochila
        finalScreenList.InsertNodeAtEnd(finalScreens[5]); // Final Ascensor
        finalScreenList.InsertNodeAtEnd(finalScreens[6]); // Final Tiempo
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Puerta")
        {
            CheckEndCondition();
        }
        else if (collision.gameObject.tag == "Salida Secreta")
        {
            CheckSecretExitCondition();
        }
        else if (collision.gameObject.CompareTag("Ascensor"))
        {
            CheckElevatorCondition();
        }
    }

    private void CheckEndCondition()
    {
        string condition = DetermineCondition();
        Tree<string>.TreeNode rootNode = endingsTree.GetRoot();
        string finalName = endingsTree.FindFinal(rootNode, condition);

        ShowFinalScreen(finalName);
    }
    private void CheckSecretExitCondition()
    {
        string condition = DetermineCondition();

        if (isTimeUp)
        {
            ShowFinalScreen("Final Tiempo");
            return;
        }
        GameObject firstItem = Listadeobjetos.GetComponent<ListadeObjetos>().GetFirstItem();
        if (firstItem != null && firstItem.name == "Tendedero")
        {
            Debug.Log("Returning 'Final Secreto'");
            ShowFinalScreen("Final Secreto");
        }
    }
    private void CheckElevatorCondition()
    {
        string condition = DetermineCondition();

        if (condition == "Final Ascensor")
        {
            ShowFinalScreen("Final Ascensor");
        }
    }
    private string DetermineCondition()
    {
       
        if (play.velocity == 5)
        {
            Debug.Log("Returning 'Final Rápido'");
            return "Final Rápido";
        }
        else if (play.velocity <= 3)
        {
            GameObject firstItem = Listadeobjetos.GetComponent<ListadeObjetos>().GetFirstItem();
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
            return "Final Rápido";
        }
    }

    private void ShowFinalScreen(string finalName)
    {
        GameObject finalScreen = null;

        switch (finalName)
        {
            case "Final Normal":
                finalScreen = finalScreenList.ObtainNodeAtPosition(0);
                break;
            case "Final Secreto":
                finalScreen = finalScreenList.ObtainNodeAtPosition(1);
                break;
            case "Final Rápido":
                finalScreen = finalScreenList.ObtainNodeAtPosition(2);
                break;
            case "Final Celular":
                finalScreen = finalScreenList.ObtainNodeAtPosition(3);
                break;
            case "Final Mochila":
                finalScreen = finalScreenList.ObtainNodeAtPosition(4);
                break;
            case "Final Ascensor":
                finalScreen = finalScreenList.ObtainNodeAtPosition(5);
                break;
            case "Final Tiempo":
                finalScreen = finalScreenList.ObtainNodeAtPosition(6);
                break;
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
    private void UpdateTimeText()
    {
        textoTiempo.text = "Tiempo Restante: " + Mathf.RoundToInt(tiempoRestante).ToString();
    }
    private void HandleTimeUp()
    {
        ShowFinalScreen("Final Tiempo");
    }
}
