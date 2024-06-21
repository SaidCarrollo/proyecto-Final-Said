using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class DtoWeennAnimationm : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button buttonToAnimate;
    public float scaleFactor = 1.1f; 
    public float duration = 0.2f; 

    private Vector3 originalScale; 

    void Start()
    {
        originalScale = buttonToAnimate.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        buttonToAnimate.transform.DOScale(originalScale * scaleFactor, duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        buttonToAnimate.transform.DOScale(originalScale, duration);
    }
}
