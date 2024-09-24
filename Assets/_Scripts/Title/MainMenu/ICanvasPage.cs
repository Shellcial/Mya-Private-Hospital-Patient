using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ICanvasPage : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    private const float fadeDuration = 0.5f;
    public void EnterPage(float duration = fadeDuration){
        _canvasGroup.DOFade(1, duration);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void LeavePage(float duration = fadeDuration){
        _canvasGroup.DOFade(0, duration);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
