using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SceneManager_Entrance : MonoBehaviour, ISceneManager
{
    public Vector3 playerStartPosition{
        get{
            return new Vector3(13.5f,0.776f,-6.203f);
        }
    }

    public Vector3 playerStartRotation{
        get{
            return new Vector3(0,180,0);
        }
    }

    public Vector3 playerCameraStartPosition{
        get{
            return new Vector3(0,0.33f,0);
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
        GameManager.Instance.PauseGame();
    }

    void Start()
    {

    }
}
