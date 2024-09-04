using System.Collections;
using System.Collections.Generic;
using Tayx.Graphy.Audio;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    [SerializeField]
    private List<Light> insideLights = new List<Light>(); 
    [SerializeField]
    private List<Light> corridorLights = new List<Light>();
    private float _warmLight = 5500f;
    private float _coldLight = 20000f;
    [SerializeField]
    private List<RotateLightFrame> _rotateLightFrame = new List<RotateLightFrame>();
    // Start is called before the first frame update

    private bool _isInsideLightOn = true;
    [SerializeField]
    private Renderer _corridorLight;
    [SerializeField]
    private Renderer _insideLight;
    [SerializeField]
    private List<Renderer> foodMaterial = new List<Renderer>();
    private Vector4 _correctFoodColor = new Vector4(0.2f, 0.2f, 0.2f);
    private float _coldIntensity = 2f;
    private float _originalIntensity = 7f;
    private Vector4 _emissionColor = new Vector4(0.7490196f, 0.6078432f, 0.4941176f);
    private bool isColdLightOn = false;
    
    private void Start(){
        insideLights.ForEach(light => {
            light.intensity = 1.5f;
            light.color = new Color(0.8245283f, 0.8652258f, 1, 1);
            light.colorTemperature = _warmLight;
            _insideLight.sharedMaterial.SetVector("_EmissionColor", _emissionColor * _originalIntensity);
        });

        corridorLights.ForEach(light => {
            light.intensity = 0.7f;
            light.color = new Color(0.8245283f, 0.8652258f, 1, 1);
            light.colorTemperature = _warmLight;
            _corridorLight.sharedMaterial.SetVector("_EmissionColor", _emissionColor * _originalIntensity);
        });
    }

    public void ShowCorrectLight(){
        isColdLightOn = true;

        insideLights.ForEach(light => {
            light.intensity = 0.1f;
            light.color = new Color(0.8245283f, 0.8652258f, 1, 1);
            light.colorTemperature = _coldLight;
            light.enabled = true;
            _insideLight.sharedMaterial.SetVector("_EmissionColor", _emissionColor * _coldIntensity);
        });

        corridorLights.ForEach(light => {
            light.intensity = 1f;
            light.color = new Color(0.8245283f, 0.8652258f, 1, 1);
            light.colorTemperature = _coldLight;
            _corridorLight.sharedMaterial.SetVector("_EmissionColor", _emissionColor * _originalIntensity);
        });

        // enable rotate script
        _rotateLightFrame.ForEach(lightframe => lightframe.enabled = true);

        // change material emission color
        foodMaterial.ForEach(_foodMaterial => {
            _foodMaterial.material.SetVector("_emission_intensity", _correctFoodColor);
        });
    }

    public void  SwitchLight(){
        if (isColdLightOn) return;

        _isInsideLightOn = !_isInsideLightOn;

        insideLights.ForEach(light => {
            light.enabled = _isInsideLightOn;
            if (_isInsideLightOn){
                _insideLight.sharedMaterial.SetVector("_EmissionColor", _emissionColor * _originalIntensity);
            }
            else{
                _insideLight.sharedMaterial.SetVector("_EmissionColor", _emissionColor * 0);
            }
        });
    }
}
