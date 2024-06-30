using Cysharp.Threading.Tasks;
using UnityEngine;

public class SceneManager_HospitalEntrance : MonoBehaviour, ISceneManager
{
    public Vector3 playerStartPosition{
        get{
            // return new Vector3(15.281f,0.776f,-17.43f);
            return new Vector3(-14.29f,0.776f,3.64f);
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

    void Awake(){
        player = GameObject.Find("Player");

        player.transform.localPosition = playerStartPosition;
        player.transform.localRotation = Quaternion.Euler(playerStartRotation);

        cameraPlayer = player.transform.Find("Character_Camera");
        cameraPlayer.localPosition = playerCameraStartPosition;
        cameraPlayer.localRotation = Quaternion.Euler(playerCameraStartRotation);

        GeneralUIManager.Instance.SetBlack();
        GameManager.Instance.PauseGame();
        GameManager.Instance.LockCursor(true);
    }

    [SerializeField]
    private Animator machineGunAnimator;    

    public async UniTask Start()
    {
        GeneralUIManager.Instance.FadeOutBlack(2f).Forget();
        await UniTask.Delay(1000);
        GameManager.Instance.ResumeGame();
        machineGunAnimator.speed = 1;
    }
}
