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

    //let other script to reference this camera (since player can only trigger one event at the same time)
    public GameObject cameraEvent;

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        LockCursor(true);
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        string savePath = Application.persistentDataPath + "/Save";
        if (System.IO.Directory.Exists(savePath))
        {
            Debug.Log("file exist: " + savePath);
        }
        else
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Save");
            Debug.Log("file does not exist and is created: " + savePath);
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
        //cursorDisplay.transform.localPosition = Mouse.current.position.ReadValue();
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
