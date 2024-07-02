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
    public SimplyLinkedList<float> endingTimesList = new SimplyLinkedList<float>();
    public SimplyLinkedList<float> shortestTimesList = new SimplyLinkedList<float>();
    public TextMeshProUGUI textoTiemposCortos;
    public GameObject Tiempos;
    public AudioSource Terremotosfx;
    public Beast3 tiemposData;
    private const int MaxShortestTimes = 3;
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
                finalScreen = finalScreenList.ObtainNodeAtPosition(0);//O(1) Al ser una lista simple el acceso es directo
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
        }//O(1) Ya que solo es seleccionar una pantalla basado en su nombre y los casos son finitos

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
    }//O(1)Porque todas las operaciones realizadas dentro son constantes y no dependen del tamaño de ninguna estructura de datos ni de un bucle, nisuiqera ObtainnodeAtPosition tiene un bucle como un while o for
    private void RecordEndTime(string finalName)
    {
        float remainingSeconds = tiempoRestante;

        Debug.Log($"Registrando tiempo sobrante {remainingSeconds} para el final: {finalName}");
        PlayerPrefs.SetFloat(finalName + "RemainingTime", remainingSeconds);
        PlayerPrefs.Save();

        // Insertar y ordenar el nuevo tiempo
        InsertAndSortShortestTime(remainingSeconds);
    }
    private void InsertAndSortShortestTime(float newTime)
    {
        float[] times = new float[MaxShortestTimes];

        for (int i = 0; i < MaxShortestTimes; i++)
        {
            string key = $"ShortestTime{i + 1}";
            times[i] = PlayerPrefs.GetFloat(key, float.MaxValue); 
        }

        times[MaxShortestTimes - 1] = newTime;
        BubbleSort(times);

        for (int i = 0; i < MaxShortestTimes; i++)
        {
            string key = $"ShortestTime{i + 1}";
            PlayerPrefs.SetFloat(key, times[i]);
        }

        PlayerPrefs.Save();

        UpdateShortestTimesText();
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
                if (numbers[j] < numbers[j + 1])
                {
                    tmp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = tmp;
                    swapped = true;
                }
            }

            if (!swapped)
            {
                break;
            }
        }
    }
    private void LoadShortestTimes()
    {
        for (int i = 0; i < MaxShortestTimes; i++)
        {
            string key = $"ShortestTime{i + 1}";
            float savedTime = PlayerPrefs.GetFloat(key, float.MaxValue);
            shortestTimesList.InsertNodeAtEnd(savedTime);
        }
    }

    private void UpdateShortestTimesText()
    {
        if (textoTiemposCortos != null)
        {
            textoTiemposCortos.text = "Tiempos más cortos:\n";
            for (int i = 0; i < Mathf.Min(MaxShortestTimes, shortestTimesList.length); i++)
            {
                float time = shortestTimesList.ObtainNodeAtPosition(i);
                if (time > 0)
                {
                    textoTiemposCortos.text += (i + 1) + ". " + time.ToString("F2") + " segundos\n";
                }
                else
                {
                    textoTiemposCortos.text += (i + 1) + ". --\n";
                }
            }
        }
        else
        {
            Debug.LogWarning("El objeto de texto para los tiempos más cortos no está asignado en el Inspector.");
        }
    }

    public void ResetTimes()
    {
        PlayerPrefs.DeleteAll();
        shortestTimesList.Clear(); 
        UpdateShortestTimesText();
    }

    private void UpdateTimeText()
    {
        textoTiempo.text = "Tiempo Restante: " + Mathf.RoundToInt(tiempoRestante).ToString();
    }

    private void HandleTimeUp()
    {
        Terremotosfx.Stop();
        Terremotosfx.Play();
        Time.timeScale = 0;
        Tiempos.SetActive(true);
        libreta.SetActive(false);
    }

}
