using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class changes the screen lights color continuously
public class ScreenLightChange : MonoBehaviour
{

    float changeDuration = 0f;
    float repeatTime = 2f;
    float range = 0.05f;
    float changeValue = 0f;
    Light referenceLight;
    List<Light> otherLights = new List<Light>(); 
    // Start is called before the first frame update
    void Start()
    {
        referenceLight = GameObject.Find("Screen_Light_reference").GetComponent<Light>();
        Light displayLight = GameObject.Find("Screen_Light_display").GetComponent<Light>();
        Light rimLight = GameObject.Find("Rim_Light").GetComponent<Light>();
        Light saltWaterStandLight  = GameObject.Find("salt_water_stand_light").GetComponent<Light>();;
        Light screenBeamLight  = GameObject.Find("Screen_Spotlight_and_Beam").GetComponent<Light>();

        otherLights.Add(displayLight);
        otherLights.Add(rimLight);
        otherLights.Add(saltWaterStandLight);
        otherLights.Add(screenBeamLight);
        changeValue = UnityEngine.Random.Range(-range,range);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (changeDuration > repeatTime){
            changeDuration = 0f; 
            changeValue = UnityEngine.Random.Range(-range,range);
        }
        else{
            changeDuration += Time.deltaTime;
        }
        
        float currentScale = 1 / repeatTime * changeDuration;
        // alter a up or down 
        
        float redValue = referenceLight.color.r;
        float targetValueR = Mathf.Clamp(Mathf.Lerp(redValue, redValue+changeValue, currentScale),0,1);
        float greenValue = referenceLight.color.g;
        float targetValueG = Mathf.Clamp(Mathf.Lerp(greenValue, greenValue+changeValue, currentScale),0,1);
        float blueValue = referenceLight.color.b;
        float targetValueB = Mathf.Clamp(Mathf.Lerp(blueValue, blueValue+changeValue, currentScale),0,1);

        Color finalColor = new Color(targetValueR, targetValueG, targetValueB, 1);

        foreach(Light _light in otherLights){
            _light.color = finalColor;
        }
    }
}
