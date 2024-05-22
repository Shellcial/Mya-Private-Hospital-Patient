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
    float frequencySecond = 0f;
    [SerializeField]
    Light referenceLight, displayLight, rimLight, saltWaterStandLight, screenBeamLight;
    List<Light> otherLights = new List<Light>(); 
    // Start is called before the first frame update
    void Start()
    {
        otherLights.Add(displayLight);
        otherLights.Add(rimLight);
        otherLights.Add(saltWaterStandLight);
        otherLights.Add(screenBeamLight);
        frequencySecond = UnityEngine.Random.Range(-range,range);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (changeDuration > repeatTime){
            changeDuration = 0f; 
            frequencySecond = UnityEngine.Random.Range(-range,range);
        }
        else{
            changeDuration += Time.deltaTime;
        }
        
        float currentScale = 1 / repeatTime * changeDuration;
        // alter a up or down 
        
        float redValue = referenceLight.color.r;
        float targetValueR = Mathf.Clamp(Mathf.Lerp(redValue, redValue+frequencySecond, currentScale),0,1);
        float greenValue = referenceLight.color.g;
        float targetValueG = Mathf.Clamp(Mathf.Lerp(greenValue, greenValue+frequencySecond, currentScale),0,1);
        float blueValue = referenceLight.color.b;
        float targetValueB = Mathf.Clamp(Mathf.Lerp(blueValue, blueValue+frequencySecond, currentScale),0,1);

        Color finalColor = new Color(targetValueR, targetValueG, targetValueB, 1);

        foreach(Light _light in otherLights){
            _light.color = finalColor;
        }
    }
}
