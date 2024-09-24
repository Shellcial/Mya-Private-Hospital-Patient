using UnityEngine;
using UnityEngine.EventSystems;

public class BTSHoverCancel : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        TitleUIManager.Instance.bTSPageManager.ExitHoverBTS();
    }
}
