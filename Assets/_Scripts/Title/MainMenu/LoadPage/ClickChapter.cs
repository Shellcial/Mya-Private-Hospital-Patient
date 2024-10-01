using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickChapter : MonoBehaviour
{
    public async void Onclick(){
        // GLogger.Log("click chapter");
        if (!TitleUIManager.Instance.isEnterChapter){
            TitleUIManager.Instance.EnteringChapter();
            await GeneralUIManager.Instance.FadeInBlack();
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
