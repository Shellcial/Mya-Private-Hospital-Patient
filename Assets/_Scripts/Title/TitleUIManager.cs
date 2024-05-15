using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    async void Awake(){
        GeneralUIManager.Instance.SetBlack();
        await GeneralUIManager.Instance.FadeOutBlack();
    }

    public void ChooseLoad(){

    }

    public void OpenSetting(){

    }

    public void OpenBlooper(){

    }
}
