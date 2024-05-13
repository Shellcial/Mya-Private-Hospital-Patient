using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    //General UI setting
    private GameObject androidControlCanvas;
    public bool isAndroidPlatform;

    //game status control
    private bool isPlayerMovable;
    private bool isPointerDirectControlAllowed;
    private bool isGameStarted;

    //whether palyer is holding meals
    public bool isHoldingMeals;
    public bool isHoldingCup;
    public bool isHoldingCupPlate;

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
        //set canvas between android and window platform
        isAndroidPlatform = false;
        androidControlCanvas = GameObject.Find("Canvas_TouchControl");
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            androidControlCanvas.SetActive(false);
            Debug.Log("on WindowPlayer");
        }

        // cameraEvent = GameObject.Find("Camera_Event");
        // cameraEvent.SetActive(false);
       
        // SetStartStatus();
        LockCursor(true);
    }

    void Start()
    {
        string savePath = Application.persistentDataPath + "/Save";
        if (System.IO.Directory.Exists(savePath))
        {
            // Debug.Log("file exist: " + savePath);
        }
        else
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Save");
            // Debug.Log("file does not exist and is created: " + savePath);
        }
       
        //development only
        Debug.Log(GraphicsSettings.currentRenderPipeline);
    }

    public void EnableAndroidCanvas(bool isEnable)
    {
        if (isAndroidPlatform)
        {
            if (isEnable)
            {
                androidControlCanvas.SetActive(true);
            }
            else
            {
                androidControlCanvas.SetActive(false);
            }
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
