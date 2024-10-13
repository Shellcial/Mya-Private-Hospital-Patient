using UnityEngine;
using UnityEngine.Audio;

public class GameManager : Singleton<GameManager>
{
    //game status control
    private bool isPlayerMovable;
    private bool isPointerDirectControlAllowed;
    private bool isGameStarted;
    public GameDataManager gameDataManager;
    private AudioMixer _masterMixer;

    override protected void Awake(){
        if( !Instance )
		{
			Instance = this;
            gameDataManager = GetComponent<GameDataManager>();
			DontDestroyOnLoad(gameObject);
		}
		else if( Instance != this )
		{
			Destroy(gameObject);
			return;
		}

        GLogger.SetLogLevel(GLogger.LogLevel.Info);
        _masterMixer = Resources.Load<AudioMixer>("Master");
        // GameManager.Instance.gameDataManager.gameData.isGetReceptionKey = false;
        // GameManager.Instance.gameDataManager.gameData.isGetReceptionKey = true;
        // GameManager.Instance.gameDataManager.SaveGame();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update(){
        // for debug only
        if (Input.GetKeyUp(KeyCode.L)){
            gameDataManager.LogWholeSaving();
        }

        if (Input.GetKeyUp(KeyCode.S)){
            gameDataManager.SaveGame();
        }
    }
    
    public void LockCursor(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void EnablePointerControl(bool isEnable)
    {
        if (isEnable)
        {
            isPointerDirectControlAllowed = true;
        }
        else
        {
            isPointerDirectControlAllowed = false;
        }
    }

    public bool GetGameStatus()
    {
        return isGameStarted;
    }

    public bool GetPlayerStatus()
    {
        return isPlayerMovable;
    }

    public void PauseGame()
    {
        isGameStarted = false;
        FreezePlayer();
    }

    public void ResumeGame()
    {
        isGameStarted = true;
        ResumePlayer();
    }

    public void FreezePlayer()
    {
        MenuController.Instance.CheckMenu();
        FreezePlayerMove();
    }

    public void FreezePlayerMove(){
        PlayerController.Instance.HideCursor();
        PlayerController.Instance.ZoomOut();
        isPlayerMovable = false;
    }

    public void ResumePlayer()
    {
        isPlayerMovable = true;
    }


    public bool GetPointerControlStatus()
    {
        return isPointerDirectControlAllowed;
    }

    public void FadeInAudioMixer(float duration){
        StartCoroutine(FadeMixerGroup.StartFade(_masterMixer, "MasterVolume", duration, 1f));
    }

    public void FadeOutAudioMixer(float duration){
        StartCoroutine(FadeMixerGroup.StartFade(_masterMixer, "MasterVolume", duration, 0f));
    }
}
