using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void ClickLink(string url){
        UIAudioManager.Instance.Play("simple_click", true);
         Application.OpenURL(url);
    }
}
