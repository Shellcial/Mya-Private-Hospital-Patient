using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        UIAudioManager.Instance.Play("exit", true);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        UIAudioManager.Instance.Play("highlight", true);
    }
}