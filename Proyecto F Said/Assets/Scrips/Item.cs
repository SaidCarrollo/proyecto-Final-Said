using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Item : MonoBehaviour
{
    public string itemName ;
    public int priority ;
    [SerializeField]private Color auraColor = Color.white;
    public float auraScaleMultiplier = 1.2f;
    public float duration = 1f;

    private Vector3 originalScale;
    private Color originalColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalScale = transform.localScale;
        originalColor = rend.material.color;
        Sequence auraSequence = DOTween.Sequence();
        auraSequence.Append(transform.DOScale(originalScale * auraScaleMultiplier, duration / 2).SetEase(Ease.OutQuad));
        auraSequence.Join(rend.material.DOColor(auraColor, duration / 2).SetEase(Ease.OutQuad));
        auraSequence.Append(transform.DOScale(originalScale, duration / 2).SetEase(Ease.InQuad));
        auraSequence.Join(rend.material.DOColor(originalColor, duration / 2).SetEase(Ease.InQuad));
        auraSequence.SetLoops(-1);
    }
}
