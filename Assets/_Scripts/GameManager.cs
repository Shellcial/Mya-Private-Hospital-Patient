using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : Singleton<GameManager>
{
    //game status control
    private bool isPlayerMovable;
    private bool isPointerDirectControlAllowed;
    private bool isGameStarted;
    public GameDataManager gameDataManager;
    override protected void Awake(){
        if( !Instance )
		{
			Instance = this;
            gameDataManager = GetComponent<GameDataManager>();
			DontDestroyOnLoad( gameObject );
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
			return;
		}
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update(){
        // for debug only
        if (Input.GetKeyUp(KeyCode.S)){
            gameDataManager.gameData.isGetReceptionKey = true;
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
}
