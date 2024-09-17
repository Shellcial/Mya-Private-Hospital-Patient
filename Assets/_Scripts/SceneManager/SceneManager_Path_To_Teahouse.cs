using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Path_To_Teahouse : Singleton<SceneManager_Path_To_Teahouse>
{
	protected override void Awake()
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

		GeneralUIManager.Instance.SetBlack();
		GameManager.Instance.PauseGame();
		GameManager.Instance.LockCursor(true);
	}

    async void Start(){
		GameManager.Instance.FadeInAudioMixer(2f);
		await Task.Delay(1000);
		DialogueManager.Instance.ShowDialogueText(true);
		await Task.Delay(500);
		GetComponent<PathToTeahouseDialogue>().ClickSentence();
		await Task.Delay(100);
		DialogueManager.Instance.isDialogueEnable = true;
    }

	public async void ShowPathVisual(){
		DialogueManager.Instance.ClearText();
		DialogueManager.Instance.ShowDialogueText(false, 0f);
		FlatAudioManager.instance.SetAndFade("bird", 2f, 0f, 1f);
		DialogueManager.Instance.isDialogueEnable = false;
		await GeneralUIManager.Instance.FadeOutBlack(2f);
		await Task.Delay(500);
		DialogueManager.Instance.ShowDialogueText(true);
		await Task.Delay(500);
		GetComponent<PathToTeahouseDialogue>().ClickSentence();
		await Task.Delay(100);
		DialogueManager.Instance.isDialogueEnable = true;
	}

	public async void ShowRoadSign(){
		DialogueManager.Instance.ClearText();
		DialogueManager.Instance.ShowDialogueText(false, 0f);
		DialogueManager.Instance.isDialogueEnable = false;
		DialogueManager.Instance.ShowCloseUp(true, 2f);
		await Task.Delay(2500);
		DialogueManager.Instance.ShowDialogueText(true);
		await Task.Delay(500);
		GetComponent<PathToTeahouseDialogue>().ClickSentence();
		await Task.Delay(100);
		DialogueManager.Instance.isDialogueEnable = true;
	}

    public async void SwitchScene(){
		DialogueManager.Instance.ClearText();
		DialogueManager.Instance.isDialogueEnable = false;
		GameManager.Instance.FadeOutAudioMixer(2f);
		DialogueManager.Instance.ShowDialogue(false,1f);
		await GeneralUIManager.Instance.FadeInBlack();
        SceneManager.LoadScene("Outside_Teahouse");
    }
}
