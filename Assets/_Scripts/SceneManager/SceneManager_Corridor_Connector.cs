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

public class SceneManager_Corridor_Connector : Singleton<SceneManager_Corridor_Connector>, ISceneManager
{
    public Vector3 playerStartPosition{
        get{
            return new Vector3(-50.94f,2.975521f, 29.62f);
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
    
    override protected void Awake(){
		if( !Instance )
		{
			Instance = this;
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

    async UniTask Start()
    {
        GameManager.Instance.FadeInAudioMixer(2f);
        await GeneralUIManager.Instance.FadeOutBlack(2f);
        GameManager.Instance.ResumeGame();
        // FlatAudioManager.instance.SetAndFade("ambience_horror", 2f, 0f, 0.1f);
    }

    
    public void InitializeScene()
    {
        GameManager.Instance.gameDataManager.UnlockScene("Hospital_corridor");
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("Hospital_General_Ward");
    }

}
