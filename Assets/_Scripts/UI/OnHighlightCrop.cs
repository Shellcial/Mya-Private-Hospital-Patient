using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHighlightCrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image _cropImage;
    private Tween _fade;
    void Awake(){
        _cropImage.fillAmount = 0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _cropImage.fillAmount = 0f;
        if (_fade != null){
            _fade.Kill();
        }
        _fade = _cropImage.DOFillAmount(1, 0.4f).SetEase(Ease.OutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_fade != null){
            _fade.Kill();
        }
        _cropImage.fillAmount = 0f;
    }
}
