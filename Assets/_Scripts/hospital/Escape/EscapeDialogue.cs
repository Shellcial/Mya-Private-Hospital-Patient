using System.Collections.Generic;
using UnityEngine.InputSystem;

public class EscapeDialogue : AbstractInputActionsController
{
    private List<string> dialogueList = new List<string>(){
        "剛剛的熊貓是怎麼一回事？",
        "還是快點離開這兒吧。",
    };

    private int currentSentence = 0;

    void Awake()
    {
        InitiateInputActions();
        playerInput.actions["Click"].performed += LeftClick;
    }

    public void LeftClick(InputAction.CallbackContext context){
        ClickSentence();
    }

    public void ClickSentence(bool isByPass = false){
        
        if (!GameManager.Instance.GetPlayerStatus()){
            return;
        }

        if (!isByPass){
            if (!DialogueManager.Instance.isDialogueEnable){
                return;
            }
        }

        if (currentSentence <= dialogueList.Count){
            if (DialogueManager.Instance.isSentencePlaying){
                DialogueManager.Instance.JumpSentence(dialogueList[currentSentence-1]);
            }
            else{
                switch (currentSentence){
                    case 2:
                        SceneManager_Escape.Instance.SwitchScene();
                        break;
                    default:
                        // GLogger.Log("Show dialogue: " + dialogueList[currentSentence]);
                        DialogueManager.Instance.ShowNextSentence(dialogueList[currentSentence]);
                        break;
                } 
                currentSentence++;
            }
        }
    }

    void OnDestroy(){
        playerInput.actions["Click"].performed -= LeftClick;
    }
}
