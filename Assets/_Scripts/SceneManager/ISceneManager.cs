using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneManager
{
    Vector3 playerStartPosition{get;}
    Vector3 playerStartRotation{get;}
    Vector3 playerCameraStartPosition{get;}    
    Vector3 playerCameraStartRotation{get;}
    Vector3 sceneCameraStartPosition{get;}
    Vector3 sceneCameraStartRotation{get;}
}
