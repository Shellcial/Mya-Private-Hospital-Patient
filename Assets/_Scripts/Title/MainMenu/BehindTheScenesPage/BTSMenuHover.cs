using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BTSMenuHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private int _buttonIndex;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TitleUIManager.Instance.bTSPageManager.OnHoverBTS(_buttonIndex);
    }
}
