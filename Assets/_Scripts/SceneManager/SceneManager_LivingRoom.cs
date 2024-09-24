using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_LivingRoom : Singleton<SceneManager_LivingRoom>
{
    public bool isGetElectricKey { get; private set; }

    void Start()
    {
        isGetElectricKey = false;
    }

    public void GetElectricKey(){
        isGetElectricKey = true;
    }

    public async void SwitchScene()
    {
        GameManager.Instance.PauseGame();
        GameManager.Instance.FadeOutAudioMixer(2f);
        await GeneralUIManager.Instance.FadeInBlack(2f);
        SceneManager.LoadScene("Hospital_Leave");
    }
}
