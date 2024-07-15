using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneManager_PooRoom : Singleton<SceneManager_PooRoom>, ISceneManager
{
    public Vector3 playerStartPosition{
        get{
            return new Vector3(-0.239f,0.529f,-0.19f);
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

    [SerializeField]
    private RuntimeAnimatorController _referenceAnimator;
    Animator _addedAnimator;

    [SerializeField]
    private VideoPlayer _videoPlayer;
    [SerializeField]
    private VideoClip _videoClipEnded;
    [SerializeField]
    private GameObject videoScreen;
    [SerializeField]
    private PlayableDirector _doorDirector;

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
        await UniTask.Delay(3000);
        GeneralUIManager.Instance.FadeOutBlack(6f).Forget();
        GameManager.Instance.ResumeGame();
        
    }

    public void InitializeScene(){
        player = GameObject.Find("Player");

        player.transform.localPosition = playerStartPosition;
        player.transform.localRotation = Quaternion.Euler(playerStartRotation);

        cameraPlayer = player.transform.Find("Character_Camera");
        cameraPlayer.localPosition = playerCameraStartPosition;
        cameraPlayer.localRotation = Quaternion.Euler(playerCameraStartRotation);

        if (player.GetComponent<Animator>() != null){
            Destroy(player.GetComponent<Animator>());
        }
        _addedAnimator = player.AddComponent<Animator>();
        _addedAnimator.runtimeAnimatorController = _referenceAnimator;

        _videoPlayer.loopPointReached += NaturalEndVideo;
        StartCoroutine(PlayVideo());
    }

    
    private void NaturalEndVideo(VideoPlayer _vp){
        // play panda converted video, stop input
        _videoPlayer.clip = _videoClipEnded;
        _videoPlayer.Play();
        GameManager.Instance.gameDataManager.gameData.isPooRoomVideoForcelyStopped = false;
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor(){
        GetComponent<ControllerPassword>().StopPassword();
        yield return new WaitForSeconds(2f);
        _doorDirector.Play();
    }

    public void ForceEndVideo(){
        GameManager.Instance.gameDataManager.gameData.isPooRoomVideoForcelyStopped = true;
        // stop video, off light, play machine down sound
        _videoPlayer.Stop();
        StartCoroutine(OpenDoor());
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("Hospital_Leave");
    }

    IEnumerator PlayVideo(){
        yield return new WaitForSeconds(21);
        player.GetComponent<CharacterController>().enabled = true;
        yield return new WaitForSeconds(3f);
        _videoPlayer.Play();
        _addedAnimator.enabled = false;
    }
}
