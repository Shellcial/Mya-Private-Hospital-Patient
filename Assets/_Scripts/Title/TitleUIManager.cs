using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [Header("FirstButton")]
    [SerializeField] GameObject startButton;
    void Awake(){
        GeneralUIManager.Instance.SetBlack();       
    }

    async void Start(){
        EventSystem.current.SetSelectedGameObject(startButton);
        await GeneralUIManager.Instance.FadeOutBlack(2f);           
    }

    public void ChooseLoad(){

    }

    public void OpenSetting(){

    }

    public void OpenBlooper(){

    }

    public void SelectButton(){

    }
}
