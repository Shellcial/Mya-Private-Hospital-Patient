using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_LivingRoom : Singleton<SceneManager_LivingRoom>, ISceneManager
{
    public Vector3 playerStartPosition{
        get{
            return new Vector3(-8.4f,0.492f, 15.517f);
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

    public bool isGetElectricKey { get; private set; }

    void Start()
    {
        isGetElectricKey = false;
    }

    public void GetElectricKey(){
        isGetElectricKey = true;
    }

    public void InitializeScene()
    {

    }

    public void SwitchScene()
    {

    }
}
