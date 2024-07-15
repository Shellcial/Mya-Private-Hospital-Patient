using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Path_To_Teahouse : Singleton<SceneManager_Path_To_Teahouse>
{
	protected virtual void Awake()
	{
		if( !Instance )
		{
			Instance = this;
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
			return;     
		}
	}

    void Start(){
        FlatAudioManager.instance.Play("bird", true);
    }

    public void SwitchScene(){
        SceneManager.LoadScene("Outside_Teahouse");
    }
}
