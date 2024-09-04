using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

//this class control the fading of audio mixer group
//singleton structure
public static class FadeMixerGroup
{
    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        
        if (duration == 0){
            audioMixer.SetFloat(exposedParam, Mathf.Log10(1) * 20);
        }
        else{
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
                audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
                yield return null;
            }
        }
        yield break;
    }
}