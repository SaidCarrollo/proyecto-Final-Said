using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsejosfinalesLista : MonoBehaviour
{
    public GameObject[] consejosPantallas; 
    public Button previousButton;
    public Button nextButton; 

    private DoubleLinkedList<GameObject> consejosLista = new DoubleLinkedList<GameObject>();
    private int currentIndex = 0;

    void Start()
    {
        for (int i = 0; i < consejosPantallas.Length; i++)
        {
            consejosPantallas[i].SetActive(false); 
            consejosLista.InsertAtEnd(consejosPantallas[i]);
        }

        MostrarConsejo(currentIndex);
        previousButton.onClick.AddListener(ShowPreviousConsejo);
        nextButton.onClick.AddListener(ShowNextConsejo);
    }

    void MostrarConsejo(int index)
    {
        DoubleLinkedList<GameObject>.Node currentNode = consejosLista.GetNodeAtPosition(currentIndex);
        if (currentNode != null)
        {
            currentNode.Value.SetActive(false);
        }

        DoubleLinkedList<GameObject>.Node newNode = consejosLista.GetNodeAtPosition(index);
        if (newNode != null)
        {
            newNode.Value.SetActive(true);
            currentIndex = index;
        }
        UpdateButtonStates();
    }//O(N), getNodeAtPosition contiene un while, osea un bucle, por lo cual lo hace 0(N)

    void ShowPreviousConsejo()
    {
        if (currentIndex > 0)
        {
            MostrarConsejo(currentIndex - 1);
        }
    }

    void ShowNextConsejo()
    {
        if (currentIndex < consejosLista.Length() - 1)
        {
            MostrarConsejo(currentIndex + 1);
        }
    }
    void OnEnable()
    {
        currentIndex = 0;
        MostrarConsejo(currentIndex);
    }

    void OnDisable()
    {
        DoubleLinkedList<GameObject>.Node currentNode = consejosLista.GetNodeAtPosition(currentIndex);
        if (currentNode != null)
        {
            currentNode.Value.SetActive(false);
        }
    }
    void UpdateButtonStates()
    {
        previousButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < consejosLista.Length() - 1;
    }//O(1), Porque simplemente actualiza el estado de los botones de navegación
}
