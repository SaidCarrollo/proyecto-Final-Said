using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ListadeObjetos : MonoBehaviour
{
    public TextMeshProUGUI inventoryText;
    public SimplyLinkedList<GameObject> inventoryList;

    private void Awake()
    {
        inventoryList = new SimplyLinkedList<GameObject>();
    }

    public void UpdateInventoryText(string newItem)
    {
        inventoryText.text += newItem + "\n";
        AddToInventory(GameObject.Find(newItem));
    }

    public void AddToInventory(GameObject newItem)
    {
        inventoryList.InsertNodeAtEnd(newItem);
    }

    public GameObject GetFirstItem()
    {
        try
        {
            return inventoryList.ObtainNodeAtStart();
        }
        catch (System.Exception)
        {
            return null;
        }
    }
}
