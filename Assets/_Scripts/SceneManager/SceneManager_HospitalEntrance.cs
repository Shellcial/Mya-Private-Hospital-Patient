using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_HospitalEntrance : Singleton<SceneManager_HospitalEntrance>, ISceneManager
{
    public Vector3 playerStartPosition{
        get{
            return new Vector3(15.281f,0.776f,-17.43f);
            // return new Vector3(-18.662f,0.776f,3.91f);
            // return new Vector3(-2.19f,0.776f,3.91f);
        }
    }

    public Vector3 playerStartRotation{
        get{
            return new Vector3(0,0,0);
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

			DontDestroyOnLoad( gameObject );
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
        GeneralUIManager.Instance.FadeOutBlack(2f).Forget();
        await UniTask.Delay(1000);
        GameManager.Instance.ResumeGame();
    }
    
    public void InitializeScene(){
        player = GameObject.Find("Player");

        player.transform.localPosition = playerStartPosition;
        player.transform.localRotation = Quaternion.Euler(playerStartRotation);

        cameraPlayer = player.transform.Find("Character_Camera");
        cameraPlayer.localPosition = playerCameraStartPosition;
        cameraPlayer.localRotation = Quaternion.Euler(playerCameraStartRotation);
        carbinetPassword = GetComponent<CarbinetPassword>();
    }

    public void GoToGummyEnding(){
        SceneManager.LoadScene("Gummy_Road");
    }

    public void GoToCredits(){
        SceneManager.LoadScene("Ending_Credits");
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("Poo_Room");
    }
}
