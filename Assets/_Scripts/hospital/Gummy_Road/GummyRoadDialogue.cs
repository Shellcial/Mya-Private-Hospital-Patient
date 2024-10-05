using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GummyRoadDialogue : AbstractInputActionsController
{
    private List<string> dialogueList = new List<string>(){
        "明明沿着來時的小徑走，但怎樣也走不出這座樹林。",
        "……",
        "……",
        "已經…走了很久了…",
        "糟糕……頭……有點暈……",
        "……",
        "……",
        "好像看到……前面有人走過來……",
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
                        SceneManager_Gummy_Road.Instance.FadeBlack1();
                        break;
                    case 3:
                        SceneManager_Gummy_Road.Instance.FadeBlack2();
                        break;
                    case 4:
                        SceneManager_Gummy_Road.Instance.FadeBlack3();
                        break;
                    case 6:
                        SceneManager_Gummy_Road.Instance.ShowGummy();
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
}
