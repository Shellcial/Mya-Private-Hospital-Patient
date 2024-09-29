using UnityEngine;
using UnityEngine.EventSystems;

public class GalleryOverlayClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData){
        TitleUIManager.Instance.galleryPageManager.ExitPhoto();
    }
}
