using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ObjectAnimation : MonoBehaviour
{
    private Button button;
    private RectTransform rectTransform;

    void Start()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.OutQuad);
        });
    }
}
