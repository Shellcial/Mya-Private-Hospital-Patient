using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_HospitalLeave : Singleton<SceneManager_HospitalLeave>, ISceneManager
{
    public Vector3 playerStartPosition{
        get{
            return new Vector3(-20.734f,0.6f,3.91f);
        }
    }

    public Vector3 playerStartRotation{
        get{
            return new Vector3(0,90,0);
        }
    }

    public Vector3 playerCameraStartPosition{
        get{
            return new Vector3(0,0.8f,0);
        }
    }

    public Vector3 playerCameraStartRotation{
        get{
            return new Vector3(0,0,0);
        }
    }

    public Vector3 sceneCameraStartPosition{
        get{
            return new Vector3(0,0,0);
        }
    }
    public Vector3 sceneCameraStartRotation{
        get{
            return new Vector3(0,0,0);
        }
    }
    
    private GameObject player;
    private Transform cameraPlayer;
    public CarbinetPassword carbinetPassword;

    override protected void Awake(){
		if( !Instance )
		{
			Instance = this;
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
			return;
		}
        
        InitializeScene();

        GeneralUIManager.Instance.SetBlack();
        GameManager.Instance.PauseGame();
        GameManager.Instance.LockCursor(true);
    }

    public async UniTask Start()
    {
        GameManager.Instance.FadeInAudioMixer(0f);
        GeneralUIManager.Instance.FadeOutBlack(2f).Forget();
        FlatAudioManager.Instance.SetAndFade("ambience_horror", 2f, 0f, 0.1f);
        PlayerController.Instance.ShowCursor();
        await UniTask.Delay(1000);
        GameManager.Instance.ResumeGame();
    }
    
    public void InitializeScene(){
        player = GameObject.Find("Player");

        player.transform.localPosition = playerStartPosition;
        PlayerController.Instance.respawnPosition = playerStartPosition;
        player.transform.localRotation = Quaternion.Euler(playerStartRotation);

        cameraPlayer = player.transform.Find("Character_Camera");
        cameraPlayer.localPosition = playerCameraStartPosition;
        cameraPlayer.localRotation = Quaternion.Euler(playerCameraStartRotation);
    }

    public void GoToGummyEnding(){
        SceneManager.LoadScene("Path_To_Teahouse_Night");
    }

    public void GoToCredits(){
        SceneManager.LoadScene("Ending_Credits");
    }

    public void SwitchScene()
    {
        // SceneManager.LoadScene("Credits");
    }
}
