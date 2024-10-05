using UnityEngine;
using UnityEngine.EventSystems;

public class NoHighLightButton : MonoBehaviour, IPointerClickHandler
{
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        UIAudioManager.Instance.Play("simple_click", true);
    }
}