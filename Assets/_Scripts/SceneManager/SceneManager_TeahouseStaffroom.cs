using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class SceneManager_TeahouseStaffroom : MonoBehaviour, ISceneManager
{
    private RawImage blackBackground;
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
        
        blackBackground = GameObject.Find("BlackForeground").GetComponent<RawImage>();
        blackBackground.enabled = true;

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
        StartCoroutine(FadeBackground(true, 0.5f, 2));
        await GeneralUIManager.Instance.FadeOutBlack();
    }

    public IEnumerator FadeBackground(bool isFadeIn, float waitTime, float fadeTime){
        yield return new WaitForSeconds(waitTime);

        if (isFadeIn){
            blackBackground.DOFade(0, fadeTime);
            if (waitTime-0.5f > 0){
                yield return new WaitForSeconds(waitTime);
                GameManager.instance.ResumeGame();
            }
            else{
                GameManager.instance.ResumeGame();
            }
        }
        else{
            GameManager.instance.PauseGame();
            blackBackground.DOFade(1, fadeTime);
        }
        
    }
}
