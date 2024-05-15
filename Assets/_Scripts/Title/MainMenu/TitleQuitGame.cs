using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleQuitGame : MonoBehaviour
{
    public async void QuitGame(){
        await GeneralUIManager.Instance.FadeInBlack();
        Application.Quit();
    }
}
