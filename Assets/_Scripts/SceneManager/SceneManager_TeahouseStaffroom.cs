using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class SceneManager_TeahouseStaffroom : MonoBehaviour, ISceneManager
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

    void Awake(){
        player = GameObject.Find("Player");

        player.transform.localPosition = playerStartPosition;
        player.transform.localRotation = Quaternion.Euler(playerStartRotation);

        cameraPlayer = player.transform.Find("Character_Camera");
        cameraPlayer.localPosition = playerCameraStartPosition;
        cameraPlayer.localRotation = Quaternion.Euler(playerCameraStartRotation);

        GeneralUIManager.Instance.SetBlack();
        GameManager.instance.PauseGame();
    }

    public async UniTask Start()
    {
        GeneralUIManager.Instance.FadeOutBlack(2f);
        await UniTask.Delay(1000);
        GameManager.instance.ResumeGame();
    }
}
