using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneManager_PooRoom : Singleton<SceneManager_PooRoom>, ISceneManager
{
    public Vector3 playerStartPosition{
        get{
            // real position
            return new Vector3(-0.239f,0.529f,-0.19f);
        }
    }

    // debug position
    private Vector3 debugStartPosition = new Vector3(5.07f,0.79f,1.2f);
    // private Vector3 debugStartPosition = new Vector3(-72.6f,0.79f,47.61723f);

    public Vector3 playerStartRotation{
        get{
            return new Vector3(0,0,0);
        }
    }

    public Vector3 playerCameraStartPosition{
        get{
            return new Vector3(0,0.5f,0);
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
    public bool isTest = false;
    [SerializeField]
    private Volume _volume;
    private GameObject player;
    private Transform cameraPlayer;

    [SerializeField]
    private RuntimeAnimatorController _referenceAnimator;
    Animator _addedAnimator;

    [SerializeField]
    private VideoPlayer _videoPlayer;

    [SerializeField]
    private PlayableDirector _doorDirector;
    [SerializeField]
    private Light _videoLight;
    [SerializeField]
    private ControllerPassword _controllerPassword;
    [SerializeField]
    private GameObject _screenBottom;
    [SerializeField]
    private GameObject _screenPart2;
    private bool _isHide = false;
    private bool _isShow = false;
    [SerializeField]
    private GameObject _sdfPanda;
    [SerializeField]
    private Animator _pandaSDFAnimator;
    private bool _isLightOn = false;
    private bool _isVideoEnded = false;
    private bool _isVideoStarted = false;
    private Color _blackColor = new Color(0,0,0,1);
    private Color _whiteColor = new Color(1,1,1,1);

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

        if (!isTest){
            GeneralUIManager.Instance.SetBlack();
            GameManager.Instance.PauseGame();
        }
        GameManager.Instance.LockCursor(true);
    }

    async UniTask Start()
    {
        GameManager.Instance.FadeInAudioMixer(0f);
        PlayerController.Instance.HideCursor(0f);
        FlatAudioManager.Instance.SetAndFade("ambience_horror2", 2f, 0f, 0.1f);
        if (!isTest){
            PlayerController.Instance.respawnPosition = playerStartPosition;
            _pandaSDFAnimator.speed = 0;
            await UniTask.Delay(1000);
            FlatAudioManager.Instance.Play("mya_wake_up", false);
            await UniTask.Delay(3000);
        }
        else{
            GameManager.Instance.ResumeGame();
            player.GetComponent<CharacterController>().enabled = true;
        }
        GeneralUIManager.Instance.FadeOutBlack(6f).Forget();
    }

    void Update(){
        if (!_isVideoEnded){
            CheckSDFStatus();
        }
    } 

    void CheckSDFStatus(){
        // check video status;
        if (_isVideoStarted && !_isVideoEnded){
            // GLogger.Log("_videoPlayer.frame: " + _videoPlayer.frame);
            if (_videoPlayer.frame >= 356){                
                if (!_isLightOn){
                    _isLightOn = true;
                    TurnOnLight();
                }
                if (_videoPlayer.frame >= 2047){
                    // hide object for particle reveal
                    if (!_isHide){
                        _isHide = true;
                        _pandaSDFAnimator.speed = 1;
                        _screenBottom.SetActive(false);
                        _screenPart2.SetActive(false);
                    }
                    
                    // revert full video plates
                    if (_videoPlayer.frame >= 2202){
                        if (!_isShow){
                            _isShow = true;
                            _screenBottom.SetActive(true);
                            _screenPart2.SetActive(true);
                            GameManager.Instance.gameDataManager.UnlockIllustration("video_room_mya");
                        }
                    }
                }
            }
        }
    }

    public void InitializeScene(){
        player = GameObject.Find("Player");

        if (!isTest){
            player.transform.localPosition = playerStartPosition;
            player.transform.localRotation = Quaternion.Euler(playerStartRotation);

            cameraPlayer = player.transform.Find("Character_Camera");
            cameraPlayer.localPosition = playerCameraStartPosition;
            cameraPlayer.localRotation = Quaternion.Euler(playerCameraStartRotation);
            //assign wake up animator to it. 
            if (player.GetComponent<Animator>() != null){
                Destroy(player.GetComponent<Animator>());
            }

            _addedAnimator = player.AddComponent<Animator>();
            _addedAnimator.runtimeAnimatorController = _referenceAnimator;

            TurnOffLight();

            _screenPart2.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", _blackColor);

            _videoPlayer.loopPointReached += NaturalEndVideo;
            StartCoroutine(PlayVideo());
        }
        else{
            player.transform.localPosition = debugStartPosition;
            player.transform.localRotation = Quaternion.Euler(playerStartRotation);

            cameraPlayer = player.transform.Find("Character_Camera");
            cameraPlayer.localPosition = playerCameraStartPosition;
            cameraPlayer.localRotation = Quaternion.Euler(playerCameraStartRotation);

            if (_volume.profile.TryGet(out Bloom bloom)){
                bloom.intensity.value = 0.1f;
            }
            else{
                GLogger.LogError("cannot get bloom volume");
            }
        }
        GameManager.Instance.gameDataManager.UnlockScene("Hospital_Poo_Room");
    }

    private void NaturalEndVideo(VideoPlayer _vp){
        // play panda converted video, stop input
        GameManager.Instance.gameDataManager.UnlockOther("watch_whole_poo_room_video");
        EndVideo();
    }
    
    public void ForceEndVideo(){
        GameManager.Instance.gameDataManager.UnlockOther("skip_poo_room_video");
        // stop video, off light, play machine down sound
        _videoPlayer.Stop();
        EndVideo();
    }

    private void EndVideo(){
        FlatAudioManager.Instance.Play("open_door", false);
        if (_volume.profile.TryGet(out Bloom bloom)){
            bloom.intensity.value = 0.1f;
        }
        else{
            GLogger.LogError("cannot get bloom volume");
        }
        TurnOffLight();
        _pandaSDFAnimator.gameObject.SetActive(false);
        // play monitor off sound
        _sdfPanda.SetActive(false);
        _screenBottom.SetActive(true);
        _screenPart2.SetActive(true);
        _isVideoEnded = true;
        _controllerPassword.StopPassword();
        _screenPart2.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", _blackColor);
        FlatAudioManager.Instance.SetAndFade("ambience_horror2", 2f, 0f, 0.1f);
        StartCoroutine(OpenDoor());
    }
    
    private IEnumerator OpenDoor(){
        yield return new WaitForSeconds(1.5f);
        _doorDirector.Play();
    }

    public async void SwitchScene()
    {
        GameManager.Instance.PauseGame();
        GameManager.Instance.FadeOutAudioMixer(2f);
        await GeneralUIManager.Instance.FadeInBlack(2f);
        SceneManager.LoadScene("General_Ward");
    }

    IEnumerator PlayVideo(){
        _videoPlayer.Prepare();
        yield return new WaitForSeconds(22);
        player.GetComponent<CharacterController>().enabled = true;
        GameManager.Instance.ResumeGame();
        PlayerController.Instance.ShowCursor();
        _addedAnimator.enabled = false;
        yield return new WaitForSeconds(5f);
        _videoPlayer.Play();
        _isVideoStarted = true;
        _screenPart2.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", _whiteColor);
        _controllerPassword.EnablePassword();
        FlatAudioManager.Instance.SetAndFade("ambience_horror2", 2f, 0.1f, 0f);
    }

    public void TurnOnLight(){
        _videoLight.enabled = true;
    }

    public void TurnOffLight(){
        _videoLight.enabled = false;
    }
}
