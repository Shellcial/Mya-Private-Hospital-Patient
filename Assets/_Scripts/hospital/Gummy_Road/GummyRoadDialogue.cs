using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GummyRoadDialogue : AbstractInputActionsController
{
    private List<string> dialogueList = new List<string>(){
        "本來想沿着來時的路回去，但怎樣也走不出這座森林。",
        "……",
        "……",
        "已經…走不動了…",
        "糟糕……頭……有點暈……",
        "……",
        "……",
        "好像……有人走過來……",
        "……"
    };

    public int currentSentence = 0;

    void Awake()
    {
        currentSentence = 0;
        InitiateInputActions();
        playerInput.actions["Click"].performed += LeftClick;
    }

    public void LeftClick(InputAction.CallbackContext context){
        ClickSentence();
    }

    
    public void ClickSentence(bool isByPass=false){
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
                    case 1:
                        SceneManager_Gummy_Road.Instance.PlayFootstepSounds(dialogueList[currentSentence]);
                        break;
                    case 2:
                        SceneManager_Gummy_Road.Instance.FadeBlack1(dialogueList[currentSentence]);
                        break;
                    case 3:
                        SceneManager_Gummy_Road.Instance.FadeBlack2(dialogueList[currentSentence]);
                        break;
                    case 4:
                        SceneManager_Gummy_Road.Instance.FadeBlack3(dialogueList[currentSentence]);
                        break;
                    case 6:
                        SceneManager_Gummy_Road.Instance.ShowGummy(dialogueList[currentSentence]);
                        break;
                    case 9:
                        SceneManager_Gummy_Road.Instance.SwitchScene();
                        break;
                    default:
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
