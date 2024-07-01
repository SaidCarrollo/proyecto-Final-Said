using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ListadeObjetos : MonoBehaviour
{
    public TextMeshProUGUI inventoryText;
    public SimplyLinkedList<GameObject> inventoryList;
    public AudioSource Lapiz;
    private void Awake()
    {
        inventoryList = new SimplyLinkedList<GameObject>();
    }

    public void UpdateInventoryText(string newItem)
    {
        
        inventoryText.text += newItem + "\n";
        AddToInventory(GameObject.Find(newItem));
        AnimateTextTypingEffect(inventoryText);
    }

    public void AddToInventory(GameObject newItem)
    {
        Lapiz.Play();
        inventoryList.InsertNodeAtEnd(newItem);
    }


    private void AnimateTextTypingEffect(TextMeshProUGUI textMeshPro)
    {
        textMeshPro.maxVisibleCharacters = 0; 

        int totalVisibleCharacters = textMeshPro.text.Length;
        DOTween.To(() => textMeshPro.maxVisibleCharacters, x => textMeshPro.maxVisibleCharacters = x, totalVisibleCharacters, 1.0f)
            .SetEase(Ease.Linear)
            .From(0) 
            .SetUpdate(true); 
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
