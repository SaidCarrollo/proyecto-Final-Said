using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public TextMeshProUGUI inventoryText;

    public Queue<GameObject> inventoryQueue;

    private void Awake()
    {
        inventoryQueue = new Queue<GameObject>();
    }

    public void UpdateInventoryText(string newItem)
    {
        inventoryText.text += newItem + "\n";
        AddToQueue(GameObject.Find(newItem));
    }

    public void AddToQueue(GameObject newItem)
    {
        inventoryQueue.Enqueue(newItem);
    }

    public GameObject GetFirstItem()
    {
        if (inventoryQueue.Count() > 0)
        {
            return inventoryQueue.GetAllValues()[0];  
        }
        return null;
    }
}
