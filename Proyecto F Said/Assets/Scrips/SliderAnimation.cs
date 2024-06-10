using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SliderInteractionAnimation : MonoBehaviour
{
    public Slider slider;

    private Vector3 originalScale;
    private bool isInteracting;

    void Start()
    {
        originalScale = slider.transform.localScale;
    }

    public void OnSliderPointerDown()
    {
        isInteracting = true;
        slider.transform.DOScale(originalScale * 1.1f, 0.2f).SetEase(Ease.OutQuad);
    }

    public void OnSliderPointerUp()
    {
        isInteracting = false;
        slider.transform.DOScale(originalScale, 0.2f).SetEase(Ease.OutQuad);
    }

    void Update()
    {
        if (isInteracting)
        {
            slider.transform.localScale = originalScale * 1.1f;
        }
    }
}
