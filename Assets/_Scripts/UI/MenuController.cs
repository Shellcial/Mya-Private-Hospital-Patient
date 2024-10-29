using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    private MainMenu_InputActions menuInputActions;
    [SerializeField]
    private CanvasGroup _wholeESCMenu; 
    [SerializeField]
    private ESCMenu _escMainMenu;
    [SerializeField]
    private TutorialMenu _tutorialMenu;
    [SerializeField]
    private LoadPageManager _loadMenu;
    private bool isAllowOpen = false;
    // use isFading to avoid instant trigger repetatedly
    public bool isFading = false;
    private bool _isMenuOpen = false;
    [SerializeField]
    private RawImage overlay;
    [SerializeField]
    private CanvasGroup leaveOverlay;
    private List<string> noESCMenuScene = new List<string>(){
        "Title_Scene",
        "Ending_Credits"
    };

    private List<string> dialogueScene = new List<string>(){
        "Path_To_Teahouse",
        "Outside_Teahouse",
        "Path_To_Teahouse_Night",
        "Escape_Dialogue"
    };

    private bool isDialogueScene = false;
    [SerializeField]
    private RawImage escMenuFade; 

    void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        isFading = false;
        leaveOverlay.blocksRaycasts = false;
        string currentSceneName = SceneManager.GetActiveScene().name; 

        isAllowOpen = true;

        foreach(string name in noESCMenuScene){
            if (currentSceneName == name){
                isAllowOpen = false;
                break;
            }
        }

        isDialogueScene = false;

        foreach(string name in dialogueScene){
            if (currentSceneName == name){
                isDialogueScene = true;
                break;
            }
        }
        escMenuFade.DOFade(0,0);
        _wholeESCMenu.alpha = 0;
        _wholeESCMenu.blocksRaycasts = false;
        _wholeESCMenu.interactable = false;
    }
    
    void Start()
    {   
        // set general canvas to behind or in front
        if (SceneManager.GetActiveScene().name == "Title_Scene"){
            GeneralUIManager.Instance.GetComponent<Canvas>().sortingOrder = 120;
        }
        else{
            GeneralUIManager.Instance.GetComponent<Canvas>().sortingOrder = -50;
        }

        menuInputActions = new MainMenu_InputActions();
        menuInputActions.Menu.Enable();
        menuInputActions.Menu.ESC.performed += TriggerMenu;
    }

    void TriggerMenu(InputAction.CallbackContext context){
        if (isAllowOpen && !isFading){
            if (context.performed){
                if (GameManager.Instance.GetGameStatus()){
                    if (_isMenuOpen){
                        CloseMenu();
                    }
                    else {
                        OpenMenu();
                    }
                }   
            }        
        }
    }

    void OpenMenu(){
        leaveOverlay.blocksRaycasts = false;
        GameManager.Instance.LockCursor(false);
        isFading = true;
        _isMenuOpen = true;
        ShowOverlay();
        _wholeESCMenu.DOFade(1, 0.2f);
        _wholeESCMenu.blocksRaycasts = true;
        _wholeESCMenu.interactable = true;
        
        GameManager.Instance.FreezePlayerMove();
        _escMainMenu.EnterPage();
        StartCoroutine(ReactivateTrigger());
    }

    public void CloseMenu(){
        if (!isDialogueScene){
            GameManager.Instance.LockCursor(true);
        }

        isFading = true;
        _isMenuOpen = false;
        _wholeESCMenu.DOFade(0, 0.2f);
        _wholeESCMenu.blocksRaycasts = false;
        _wholeESCMenu.interactable = false;
        HideOverlay();
        _escMainMenu.LeavePage();
        _tutorialMenu.LeavePage();
        _loadMenu.SpecialLeavePage();
        leaveOverlay.blocksRaycasts = true;

        GameManager.Instance.ResumePlayer();
        PlayerController.Instance.ShowCursor();
        StartCoroutine(ReactivateTrigger());
    }

    IEnumerator ReactivateTrigger(){
        yield return new WaitForSeconds(0.2f);
        isFading = false;
    }

    public void CheckMenu(){
        if (_isMenuOpen){
            CloseMenu();
        }
    }

    public void ShowOverlay(){
        if (!isDialogueScene){
            overlay.DOFade(0.7f, 0.2f);
        }
        else{
            overlay.DOFade(0.9f, 0.2f);
        }
    }

    public void HideOverlay(){
        if (!isDialogueScene){
            overlay.DOFade(0f, 0.2f);
        }
        else{
            // overlay.DOFade(0.9f, 0.2f);
        }
    }

    public async void GoBackToTitle(){
        if (!TitleUIManager.Instance.isEnterChapter){
            CoverMenu(2f);
            GameManager.Instance.FadeOutAudioMixer(2f);
            await GeneralUIManager.Instance.FadeInBlack(2f);
            SceneManager.LoadScene("Title_Scene");
        }
    }

    public void GoToSpecialChapter(){
        CoverMenu(2f);
    }

    public void SpecialOpenTutorial(){
        leaveOverlay.blocksRaycasts = false;
        GameManager.Instance.LockCursor(false);
        isFading = true;
        _isMenuOpen = true;

        _wholeESCMenu.DOFade(1, 0.2f);
        _wholeESCMenu.blocksRaycasts = true;
        _wholeESCMenu.interactable = true;
        
        
        GameManager.Instance.ResumeGame();
        GameManager.Instance.FreezePlayerMove();

        _tutorialMenu.EnterPage();
        ShowOverlay();
        StartCoroutine(ReactivateTrigger());
    }
    public void CoverMenu(float duration){
        leaveOverlay.blocksRaycasts = true;
        escMenuFade.DOFade(1,duration);
        _wholeESCMenu.blocksRaycasts = true;
        _wholeESCMenu.interactable = false;
        UIAudioManager.Instance.Play("start", true);
        TitleUIManager.Instance.isEnterChapter = true;
        isFading = true;
    }

    void OnDestroy(){
        menuInputActions.Menu.ESC.performed -= TriggerMenu;
    }
}
