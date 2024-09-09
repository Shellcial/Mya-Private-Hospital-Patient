using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Tayx.Graphy.Audio;
using UnityEngine.Audio;

public class SceneManager_TeahouseStaffroom : Singleton<SceneManager_TeahouseStaffroom>, ISceneManager
{
    public Vector3 playerStartPosition{
        get{
            return new Vector3(-0.239f,0.142f,-0.19f);
        }
    }

    public Vector3 playerStartRotation{
        get{
            return new Vector3(0,180,0);
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

    override protected void Awake(){
        InitializeScene();

        GeneralUIManager.Instance.SetBlack();
        GameManager.Instance.PauseGame();
        GameManager.Instance.LockCursor(true);
    }  

    public async UniTask Start()
    {
        GameManager.Instance.FadeInAudioMixer(0f);
        FlatAudioManager.instance.SetAndFade("ambience_wind", 2f, 0f, 0.05f);
        GeneralUIManager.Instance.FadeOutBlack(2f).Forget();
        PlayerController.Instance.ShowCursor();
        await UniTask.Delay(1000);
        GameManager.Instance.ResumeGame();
    }

    public void InitializeScene()
    {
        if( !Instance )
		{
			Instance = this;
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
			return;
		}

        player = GameObject.Find("Player");

        player.transform.localPosition = playerStartPosition;
        PlayerController.Instance.respawnPosition = playerStartPosition;
        player.transform.localRotation = Quaternion.Euler(playerStartRotation);

        cameraPlayer = player.transform.Find("Character_Camera");
        cameraPlayer.localPosition = playerCameraStartPosition;
        cameraPlayer.localRotation = Quaternion.Euler(playerCameraStartRotation);

        GameManager.Instance.gameDataManager.UnlockIllustration("food");
        GameManager.Instance.gameDataManager.UnlockScene("Teahouse");
    }

    public async void SwitchScene()
    {
        FlatAudioManager.instance.Play("exit_door", false);
        GameManager.Instance.FadeOutAudioMixer(2f);
        await GeneralUIManager.Instance.FadeInBlack(2f);
        SceneManager.LoadScene("Hospital_Entrance");
    }
}
