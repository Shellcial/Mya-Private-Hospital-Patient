using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleStartNewGame : MonoBehaviour
{
    public async void StartNewGame(){
        await GeneralUIManager.Instance.FadeInBlack();
        SceneManager.LoadScene(1);
    }
}
