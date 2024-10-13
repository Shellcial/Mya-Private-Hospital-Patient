using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditPageManager : ICanvasPage
{
    void Awake(){
        LeavePage(0f);
    }

    public void OpenLink(string link){
        UIAudioManager.Instance.Play("simple_click", true);
        Application.OpenURL(link);
    }
}
