using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class SceneManager_LivingRoom : Singleton<SceneManager_LivingRoom>
{
    public bool isGetElectricKey { get; private set; }

    protected override void Awake(){
        if( !Instance )
		{
			Instance = this;
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
			return;
		}

        GeneralUIManager.Instance.SetBlack();
        GameManager.Instance.PauseGame();
        GameManager.Instance.LockCursor(true);
        isGetElectricKey = false;
        PlayerController.Instance.respawnPosition = new Vector3(-53.5f, 0.66f, 117.124f);
    }
    async UniTask Start()
    {
        GameManager.Instance.gameDataManager.UnlockScene("Hospital_Ward");
        GameManager.Instance.FadeInAudioMixer(2f);
        PlayerController.Instance.ShowCursor();
        await GeneralUIManager.Instance.FadeOutBlack(2f);
        FlatAudioManager.Instance.SetAndFade("ambience_horror2", 2f, 0f, 0.1f);
        GameManager.Instance.ResumeGame();
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
