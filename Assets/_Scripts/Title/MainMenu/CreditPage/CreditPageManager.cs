using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditPageManager : ICanvasPage
{
    void Awake(){
        LeavePage(0f);
    }

    public void OpenLink(string link){
        GLogger.Log(link);
        Application.OpenURL(link);
    }
}
