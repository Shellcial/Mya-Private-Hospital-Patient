using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHorrorCloseup : MonoBehaviour
{
    public List<GameObject> normalLights = new List<GameObject>();
    public List<GameObject> scaryLights = new List<GameObject>();
    public static ChangeHorrorCloseup instance;
    private GameObject normalTableware;
    private GameObject scaryTableware;
    void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(this);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        normalTableware = GameObject.Find("Close_up_teahouse").transform.Find("closeup").gameObject;
        scaryTableware = GameObject.Find("Close_up_teahouse").transform.Find("closeup_scary").gameObject;
    }

    public void ChnageToHorror(){
        Debug.Log("Chnage To Horror");
        foreach(GameObject _light in normalLights){
            _light.SetActive(false);
        }
        foreach(GameObject _light in scaryLights){
            _light.SetActive(true);
        }
        normalTableware.SetActive(false);
        scaryTableware.SetActive(true);
        RenderSettings.ambientIntensity = 0.1f;

    }
    
    public void ChangeToNormal(){
        Debug.Log("Chnage To normal");
        foreach(GameObject _light in normalLights){
            _light.SetActive(true);
        }
        foreach(GameObject _light in scaryLights){
            _light.SetActive(false);
        }
        normalTableware.SetActive(true);
        scaryTableware.SetActive(false);
        RenderSettings.ambientIntensity = 0.4f;
   }
}
