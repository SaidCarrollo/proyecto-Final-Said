using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Salida : MonoBehaviour
{
    public GameObject libreta;
    public Player play;
    public GameObject Listadeobjetos;
    private Tree<string> endingsTree;
    public GameObject[] finalScreens;
    private SimplyLinkedList<GameObject> finalScreenList;
    public float tiempoMaximo = 30f; 
    private float tiempoRestante;
    private bool isTimeUp = false;
    public TextMeshProUGUI textoTiempo;
    private float[] endingTimes = new float[9];
    private float[] shortestTimes = new float[3];
    public TextMeshProUGUI textoTiemposCortos;
    public GameObject Tiempos;
    public AudioSource Terremotosfx;
    public Beast3 tiemposData;
    private void Start()
    {

        InitializeEndingsTree();
        InitializeFinalScreenList();
        tiempoRestante = tiempoMaximo;
        LoadShortestTimes();
        UpdateShortestTimesText();
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
        endingsTree.AddNode("Final Mochila llena", "Final Mochila");
        endingsTree.AddNode("Final Bruja", "root");
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
        finalScreenList.InsertNodeAtEnd(finalScreens[7]); // Final Mochila llena
        finalScreenList.InsertNodeAtEnd(finalScreens[8]); // Final Bruja
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Puerta")
        {
            
            CheckEndCondition();
          //  Terremotosfx.Stop();
        }
        else if (collision.gameObject.tag == "Salida Secreta")
        {
            
            CheckSecretExitCondition();
            //Terremotosfx.Stop();
        }
        else if (collision.gameObject.tag == "Ascensor")
        {
            
            CheckElevatorCondition();
           // Terremotosfx.Stop();
        }
    }

    private void CheckEndCondition()
    {
        string condition = DetermineCondition();
        Tree<string>.TreeNode rootNode = endingsTree.GetRoot();
        string finalName = endingsTree.FindFinal(rootNode, condition);
        RecordEndTime(finalName);
        if (finalName != "Final Rápido")
        {
            ShowFinalScreen(finalName);
        }
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
        if (firstItem != null && firstItem.name == "Escoba")
        {
            Debug.Log("Returning 'Final Bruja'");
            ShowFinalScreen("Final Bruja");
            RecordEndTime("Final Bruja");
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
       
        if (play.velocity== 5)
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
                GameObject secondItem = null;
                GameObject thirdItem = null;
                GameObject FourItem = null;
                GameObject FiveItem = null;

                if (Listadeobjetos.GetComponent<ListadeObjetos>().inventoryList.length > 1)
                {
                    secondItem = Listadeobjetos.GetComponent<ListadeObjetos>().inventoryList.ObtainNodeAtPosition(1);
                }
                if (Listadeobjetos.GetComponent<ListadeObjetos>().inventoryList.length > 2)
                {
                    thirdItem = Listadeobjetos.GetComponent<ListadeObjetos>().inventoryList.ObtainNodeAtPosition(2);
                }
                if (Listadeobjetos.GetComponent<ListadeObjetos>().inventoryList.length > 3)
                {
                    FourItem = Listadeobjetos.GetComponent<ListadeObjetos>().inventoryList.ObtainNodeAtPosition(3);
                }
                if (Listadeobjetos.GetComponent<ListadeObjetos>().inventoryList.length > 4)
                {
                    FiveItem = Listadeobjetos.GetComponent<ListadeObjetos>().inventoryList.ObtainNodeAtPosition(4);
                }

                if (secondItem != null && thirdItem != null && FourItem != null && FiveItem != null &&
                   secondItem.name == "Botella" && thirdItem.name == "Papel" &&
                   FourItem.name == "Botiquin" && FiveItem.name == "Plato")
                {
                    Debug.Log("Returning 'Final Mochila llena'");
                    return "Final Mochila llena";
                }
                else
                {
                    Debug.Log("Returning 'Final Mochila'");
                    return "Final Mochila";
                }
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
                finalScreen = finalScreenList.ObtainNodeAtPosition(7);
                break;
            case "Final Bruja":
                finalScreen = finalScreenList.ObtainNodeAtPosition(8);
                break;
        }

        if (finalScreen != null)
        {
            finalScreen.SetActive(true);
            Time.timeScale = 0;
            Tiempos.SetActive(true);
            libreta.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No se encontró una pantalla para el final: " + finalName);
        }
    }
    private void RecordEndTime(string finalName)
    {
        float endTime = tiempoRestante;
        float remainingSeconds = tiempoMaximo - endTime; // Calcula los segundos de sobra

        Debug.Log($"Registrando tiempo {endTime} para el final: {finalName}. Segundos de sobra: {remainingSeconds}");

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
                endingTimes[7] = endTime;
                break;
            case "Final Bruja":
                endingTimes[8] = endTime;
                break;
            default:
                Debug.LogWarning("Final no reconocido: " + finalName);
                return;
        }

        Debug.Log("Tiempos antes de ordenar: " + string.Join(", ", endingTimes));
        BubbleSort(endingTimes); // Usa el Bubble Sort para ordenar los tiempos
        Debug.Log("Tiempos después de ordenar: " + string.Join(", ", endingTimes));

        for (int i = 0; i < Mathf.Min(3, endingTimes.Length); i++)
        {
            shortestTimes[i] = endingTimes[i];
        }

        UpdateShortestTimesText();
        Debug.Log("Tiempos más cortos: " + string.Join(", ", shortestTimes));
    }
    private void BubbleSort(float[] numbers)
    {
        float tmp;
        bool swapped;

        for (int i = 0; i < numbers.Length - 1; i++)
        {
            swapped = false;

            for (int j = 0; j < numbers.Length - i - 1; j++)
            {
                if (numbers[j] > numbers[j + 1])
                {
                    tmp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = tmp;
                    swapped = true;
                }
            }
            Debug.Log("Estado del arreglo después de la pasada " + (i + 1) + ": " + string.Join(", ", numbers));

            if (!swapped)
            {
                break;
            }
        }
    }
    private void UpdateShortestTimesText()
    {
        if (textoTiemposCortos != null)
        {
            textoTiemposCortos.text = "Tiempos más cortos:\n";
            for (int i = 0; i < Mathf.Min(3, tiemposData.shortestTimes.Length); i++)
            {
                textoTiemposCortos.text += (i + 1) + ". " + tiemposData.shortestTimes[i].ToString("F2") + "\n";
            }
        }
        else
        {
            Debug.LogWarning("El objeto de texto para los tiempos más cortos no está asignado en el Inspector.");
        }
    }

    private void SaveShortestTimes()
    {

        for (int i = 0; i < tiemposData.shortestTimes.Length; i++)
        {
            PlayerPrefs.SetFloat("ShortestTime" + i, tiemposData.shortestTimes[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadShortestTimes()
    {
        for (int i = 0; i < tiemposData.shortestTimes.Length; i++)
        {
            tiemposData.shortestTimes[i] = PlayerPrefs.GetFloat("ShortestTime" + i, float.MaxValue);
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
    public void ResetTimes()
    {
        PlayerPrefs.DeleteAll(); 
        endingTimes = new float[9]; 
        shortestTimes = new float[3];
        UpdateShortestTimesText();
    }
}
