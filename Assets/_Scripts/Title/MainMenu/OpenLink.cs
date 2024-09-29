using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void ClickLink(string url){
         Application.OpenURL(url);
    }
}
