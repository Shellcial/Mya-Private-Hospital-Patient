using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        UIAudioManager.Instance.Play("start", true);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        UIAudioManager.Instance.Play("highlight", true);
    }
}
