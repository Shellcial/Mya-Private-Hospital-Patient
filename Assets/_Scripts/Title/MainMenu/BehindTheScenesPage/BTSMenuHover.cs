using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BTSMenuHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private int _buttonIndex;
    private Image image;
    private Color _normalColor = new Color(0.6901961f, 0.5372549f, 0.5647059f, 1f);
    private Color _highlightColor = new Color(0.8784314f, 0.4823529f, 0.5490196f, 1f);
    private Color _pressColor = new Color(0.68f, 0.3733929f, 0.425f, 1f);
    void Awake(){
        // set normal
        // image = GetComponent<Image>();
        // image.color = _normalColor;
    }

    // highlight and hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        // image.color = _highlightColor;
        TitleUIManager.Instance.bTSPageManager.OnHoverBTS(_buttonIndex);
    }

    // // press
    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     image.color = _pressColor;
    // }

    // // trigger function
    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     TitleUIManager.Instance.bTSPageManager.OpenBTS(_buttonIndex);
    // }

    // // exit button
    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     image.color = _normalColor;
    // }
}
