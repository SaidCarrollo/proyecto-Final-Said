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
    private float[] endingTimes = new float[7];
    private float[] shortestTimes = new float[3];
    public TextMeshProUGUI textoTiemposCortos;
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
        endingsTree.AddNode("Final Rápido", "root");
        endingsTree.AddNode("Final Celular", "Final Normal");
        endingsTree.AddNode("Final Mochila", "Final Normal");
        endingsTree.AddNode("Final Ascensor", "root");
        endingsTree.AddNode("Final Tiempo", "root");
        endingsTree.AddNode("Final Mochila llena", "Final Normal");
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
        finalScreenList.InsertNodeAtEnd(finalScreens[6]); // Final Mochila llena
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
        else if (collision.gameObject.tag == "Ascensor")
        {
            CheckElevatorCondition();
        }
    }

    private void CheckEndCondition()
    {
        string condition = DetermineCondition();
        Tree<string>.TreeNode rootNode = endingsTree.GetRoot();
        string finalName = endingsTree.FindFinal(rootNode, condition);
        RecordEndTime(finalName);
        ShowFinalScreen(finalName);
    }
    private void CheckSecretExitCondition()
    {
        string condition = DetermineCondition();

        if (isTimeUp)
        {
            ShowFinalScreen("Final Tiempo");
            RecordEndTime("Final Tiempo");
            return;
        }
        GameObject firstItem = Listadeobjetos.GetComponent<ListadeObjetos>().GetFirstItem();
        if (firstItem != null && firstItem.name == "Tendedero")
        {
            Debug.Log("Returning 'Final Secreto'");
            ShowFinalScreen("Final Secreto");
            RecordEndTime("Final Secreto");
        }
    }
    private void CheckElevatorCondition()
    {
        /*string condition = DetermineCondition();

        if (condition == "Final Ascensor")
        {*/
            ShowFinalScreen("Final Ascensor");
        RecordEndTime("Final Ascensor");
        //
        //}
    }
    private string DetermineCondition()
    {
       
        if (play.velocity >= 5)
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

            if (firstItem != null && firstItem.name == "Mochila")
            {
                Debug.Log("Returning 'Final Mochila'");
                return "Final Mochila";
            }
            if (firstItem != null && firstItem.name == "Mochila" )
            {
                //GameObject secondItem = Listadeobjetos.GetComponent<ListadeObjetos>().ObtainNodeAtPositions(1);

                // Verificar si el segundo ítem es una botella
                // if (secondItem != null && secondItem.name == "botella")
               // {
              //      Debug.Log("Returning 'Final Mochila Nueva'");
              //      return "Final Mochila Nueva";
               // }
             //   else
               // {
                    Debug.Log("Returning 'Final Mochila'");
                    return "Final Mochila";
               // }
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
            case "Final Mochila llena":
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
    private void RecordEndTime(string finalName)
    {
        float endTime = tiempoMaximo - tiempoRestante;

        switch (finalName)
        {
            case "Final Normal":
                endingTimes[0] = endTime;
                break;
            case "Final Secreto":
                endingTimes[1] = endTime;
                break;
            case "Final Rápido":
                endingTimes[2] = endTime;
                break;
            case "Final Celular":
                endingTimes[3] = endTime;
                break;
            case "Final Mochila":
                endingTimes[4] = endTime;
                break;
            case "Final Ascensor":
                endingTimes[5] = endTime;
                break;
            case "Final Tiempo":
                endingTimes[6] = endTime;
                break;
            case "Final Mochila llena":
                endingTimes[6] = endTime;
                break;
        }

        SelectionSortEnhanced(endingTimes);

        for (int i = 0; i < Mathf.Min(3, endingTimes.Length); i++)
        {
            shortestTimes[i] = endingTimes[i];
        }
        UpdateShortestTimesText();
        Debug.Log("Tiempos más cortos: " + string.Join(", ", shortestTimes));
    }

    private void SelectionSortEnhanced(float[] numbers)
    {
        int minId;
        float tmp;
        for (int i = 0; i < numbers.Length - 1; ++i)
        {
            minId = i;
            for (int j = i + 1; j < numbers.Length; ++j)
            {
                if (numbers[minId] > numbers[j])
                {
                    minId = j;
                }
            }
            if (minId != i)
            {
                tmp = numbers[i];
                numbers[i] = numbers[minId];
                numbers[minId] = tmp;
            }
        }
    }
    private void UpdateShortestTimesText()
    {
        if (textoTiemposCortos != null)
        {
            textoTiemposCortos.text = "Tiempos más cortos:\n";
            for (int i = 0; i < Mathf.Min(3, shortestTimes.Length); i++)
            {
                textoTiemposCortos.text += (i + 1) + ". " + shortestTimes[i].ToString("F2") + "\n";
            }
        }
        else
        {
            Debug.LogWarning("El objeto de texto para los tiempos más cortos no está asignado en el Inspector.");
        }
    }
    private void UpdateTimeText()
    {
        textoTiempo.text = "Tiempo Restante: " + Mathf.RoundToInt(tiempoRestante).ToString();
    }
    private void HandleTimeUp()
    {
        ShowFinalScreen("Final Tiempo");
        RecordEndTime("Final Tiempo");
    }
}
