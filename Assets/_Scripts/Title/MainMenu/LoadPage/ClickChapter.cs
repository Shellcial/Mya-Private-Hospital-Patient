using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickChapter : MonoBehaviour
{
    public bool isEscUsage;
    public async void Onclick(){
        int currentSelectedTexture = TitleUIManager.Instance.loadPageManager.currentSelectedTexture;
        if (TitleUIManager.Instance.loadPageManager.isTriggerable[currentSelectedTexture]){      
            if (!TitleUIManager.Instance.isEnterChapter){

                if (isEscUsage){
                    MenuController.Instance.GoToSpecialChapter();
                }
                else {
                    TitleUIManager.Instance.EnteringChapter();
                }

                GameManager.Instance.FadeOutAudioMixer(2f);
                await GeneralUIManager.Instance.FadeInBlack(2f);

                switch (TitleUIManager.Instance.loadPageManager.currentSelectedTexture){
                    case 0:
                        SceneManager.LoadScene("Path_To_Teahouse");
                        break;
                    case 1:
                        SceneManager.LoadScene("Teahouse_Staffroom");
                        break;
                    case 2:
                        SceneManager.LoadScene("Hospital_Entrance");
                        break;
                    case 3:
                        SceneManager.LoadScene("Poo_Room");
                        break;
                    case 4:
                        SceneManager.LoadScene("General_Ward");
                        break;
                    default:
                        GLogger.LogError("Not supported chapter index: " + TitleUIManager.Instance.loadPageManager.currentSelectedTexture);
                        break;
                }
            }
        }

    }
}
