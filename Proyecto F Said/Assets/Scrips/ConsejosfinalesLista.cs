using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsejosfinalesLista : MonoBehaviour
{
    private DoubleLinkedList<GameObject> consejosLista = new DoubleLinkedList<GameObject>();

    private int currentIndex = 0;

    void Start()
    {
        consejosLista.InsertAtEnd(Resources.Load<GameObject>("Consejo1"));
        consejosLista.InsertAtEnd(Resources.Load<GameObject>("Consejo2"));
        consejosLista.InsertAtEnd(Resources.Load<GameObject>("Consejo3"));

        MostrarConsejo(currentIndex);

        Button previousButton = GameObject.Find("PreviousButton").GetComponent<Button>();
        Button nextButton = GameObject.Find("NextButton").GetComponent<Button>();

        previousButton.onClick.AddListener(ShowPreviousConsejo);
        nextButton.onClick.AddListener(ShowNextConsejo);
    }

    void MostrarConsejo(int index)
    {
        if (currentIndex >= 0 && currentIndex < consejosLista.Length())
        {
            DoubleLinkedList<GameObject>.Node consejoNode = consejosLista.GetNodeAtPosition(currentIndex);
            GameObject consejoPrefab = consejoNode.Value;
            consejoPrefab.SetActive(false);
        }

        DoubleLinkedList<GameObject>.Node newNode = consejosLista.GetNodeAtPosition(index);
        GameObject newConsejo = newNode.Value;
        newConsejo.SetActive(true);

        currentIndex = index;
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
