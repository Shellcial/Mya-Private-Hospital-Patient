using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GalleryClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public int imageIndex;
    private Image overlayImage;
    private Color normalColor = new Color(0,0,0,0);
    private Color highlightedColor = new Color(0,0,0,0.5f);
    private Color pressedColor = new Color(0,0,0,0.7f);
    void Awake(){
        overlayImage = GetComponent<Image>();
        overlayImage.color = new Color(0,0,0,0);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        TitleUIManager.Instance.galleryPageManager.ChoosePhoto(imageIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        overlayImage.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        overlayImage.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        overlayImage.color = pressedColor;
    }
}
