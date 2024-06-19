using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public TextMeshProUGUI inventoryText;

    public SimplyLinkedList<GameObject> exampleList;

    private void Awake()
    {
        exampleList = new SimplyLinkedList<GameObject>();
    }

    public void UpdateInventoryText(string newItem)
    {
        inventoryText.text += newItem + "\n";
        AddToLinkedList(GameObject.Find(newItem));
    }

    public void AddToLinkedList(GameObject newItem)
    {
        exampleList.InsertNodeAtEnd(newItem);
    }
}
