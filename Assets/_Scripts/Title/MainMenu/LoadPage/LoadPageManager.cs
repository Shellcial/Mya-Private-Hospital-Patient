using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadPageManager : ICanvasPage
{
    void Awake(){
        LeavePage(0f);
    }

    public void EnterLoadPage(){
        TitleUIManager.Instance.mainPageManager.ExitHoverMainMenu();
        EnterPage();
    }
}
