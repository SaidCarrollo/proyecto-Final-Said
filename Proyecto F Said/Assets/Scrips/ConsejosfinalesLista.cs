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
    }

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
}
