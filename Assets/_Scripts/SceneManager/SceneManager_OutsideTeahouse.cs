using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneManager_OutsideTeahouse : Singleton<SceneManager_OutsideTeahouse>
{

    override protected void Awake(){
        InitializeScene();

		GeneralUIManager.Instance.SetBlack();
		GameManager.Instance.PauseGame();
		GameManager.Instance.LockCursor(true);
	}

    async void Start(){
		GameManager.Instance.FadeInAudioMixer(2f);
        FlatAudioManager.instance.SetAndFade("bird", 2f, 0f, 0.05f);
		await Task.Delay(1000);
		DialogueManager.Instance.ShowDialogueText(true);
		await Task.Delay(500);
		DialogueManager.Instance.isDialogueEnable = true;
		GetComponent<OutsideTeahouseDialogue>().ClickSentence();
    }

    public void InitializeScene()
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

        GameManager.Instance.gameDataManager.UnlockIllustration("food");
        GameManager.Instance.gameDataManager.UnlockScene("Teahouse");
    }

	public async void GetInHouse(){
		DialogueManager.Instance.ClearText();
		DialogueManager.Instance.ShowDialogueText(false, 0f);
		FlatAudioManager.instance.SetAndFade("bird", 2f, 0f, 0.05f);
		DialogueManager.Instance.isDialogueEnable = false;
		await GeneralUIManager.Instance.FadeOutBlack(2f);
		await Task.Delay(500);
		DialogueManager.Instance.ShowDialogueText(true);
		await Task.Delay(500);
		DialogueManager.Instance.isDialogueEnable = true;
		GetComponent<PathToTeahouseDialogue>().ClickSentence();
	}

    public async void SwitchScene()
    {
        DialogueManager.Instance.isDialogueEnable = false;
		GameManager.Instance.FadeOutAudioMixer(2f);
		DialogueManager.Instance.ShowDialogue(false,1f);
		await GeneralUIManager.Instance.FadeInBlack();
        SceneManager.LoadScene("Teahouse_Staffroom");
    }
}
