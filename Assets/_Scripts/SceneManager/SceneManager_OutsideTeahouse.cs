using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using DG.Tweening;

public class SceneManager_OutsideTeahouse : Singleton<SceneManager_OutsideTeahouse>
{

    [SerializeField]
    private Camera _outsideCamera;
    [SerializeField]
    private Camera _insideCamera;
    [SerializeField]
    private GameObject coffee;
    [SerializeField]
    private Renderer distortionMaterial;
    [SerializeField]
    private Light directionalLight;
    override protected void Awake(){
        InitializeScene();

		GeneralUIManager.Instance.SetBlack();
		GameManager.Instance.PauseGame();
		GameManager.Instance.LockCursor(true);
	}

    async void Start(){
		GameManager.Instance.FadeInAudioMixer(2f);
        FlatAudioManager.instance.SetAndFade("bird", 2f, 0f, 1f);
        await GeneralUIManager.Instance.FadeOutBlack(2f);
		DialogueManager.Instance.ShowDialogue(true);
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

        directionalLight.intensity = 1;

        _outsideCamera.gameObject.SetActive(true);
        _insideCamera.gameObject.SetActive(false);
    }

	public async void GetInHouse(){
		DialogueManager.Instance.ClearText();
		DialogueManager.Instance.ShowDialogue(false, 1f);
		DialogueManager.Instance.isDialogueEnable = false;
		await GeneralUIManager.Instance.FadeInBlack(1f);
        _outsideCamera.gameObject.SetActive(false);
        _insideCamera.gameObject.SetActive(true);
        directionalLight.intensity = 2;
        await Task.Delay(1000);
        await GeneralUIManager.Instance.FadeOutBlack(1f);
		DialogueManager.Instance.ShowDialogue(true);
        await Task.Delay(500);
		GetComponent<OutsideTeahouseDialogue>().ClickSentence();
        await Task.Delay(100);
		DialogueManager.Instance.isDialogueEnable = true;
	}

    public async void TakeCoffee(){
        DialogueManager.Instance.ClearText();
		DialogueManager.Instance.isDialogueEnable = false;
        coffee.SetActive(false);
        await Task.Delay(500);
        GetComponent<OutsideTeahouseDialogue>().ClickSentence();
        await Task.Delay(100);
        DialogueManager.Instance.isDialogueEnable = true;
    } 

    public async void FeelDizzy(){
        DialogueManager.Instance.ClearText();
		DialogueManager.Instance.isDialogueEnable = false;
        distortionMaterial.material.DOFloat( 0.01f, "_distortion_strength",2f);
        await Task.Delay(1000);
        GetComponent<OutsideTeahouseDialogue>().ClickSentence();
        await Task.Delay(100);
        DialogueManager.Instance.isDialogueEnable = true;
    }

    public async void GoToilet(){
        DialogueManager.Instance.ClearText();
        DialogueManager.Instance.isDialogueEnable = false;
        DialogueManager.Instance.ShowDialogue(false, 2f);
        await GeneralUIManager.Instance.FadeInBlack(2f);
        await Task.Delay(500);
        DialogueManager.Instance.ShowDialogue(true);
        await Task.Delay(500);
        GetComponent<OutsideTeahouseDialogue>().ClickSentence();
        await Task.Delay(100);
        DialogueManager.Instance.isDialogueEnable = true;
    }

    public async void SwitchScene()
    {
        DialogueManager.Instance.ClearText();
        DialogueManager.Instance.isDialogueEnable = false;
		GameManager.Instance.FadeOutAudioMixer(2f);
		DialogueManager.Instance.ShowDialogue(false,2f);
		await GeneralUIManager.Instance.FadeInBlack(2f);
        SceneManager.LoadScene("Teahouse_Staffroom");
    }
}
