using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        UIAudioManager.Instance.Play("simple_click", true);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        UIAudioManager.Instance.Play("highlight", true);
    }
}
