using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleMenuHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private int index = 0 ;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TitleUIManager.Instance.mainPageManager.OnHoverMainMenu(index);
    }
}
