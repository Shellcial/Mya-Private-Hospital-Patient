using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleQuitGame : MonoBehaviour
{
    public async void QuitGame(){
        // TitleUIManager.Instance.mainPageManager.ExitHoverMainMenu();
        GameManager.Instance.FadeOutAudioMixer(1f);
        await GeneralUIManager.Instance.FadeInBlack();
        Application.Quit();
    }
}
