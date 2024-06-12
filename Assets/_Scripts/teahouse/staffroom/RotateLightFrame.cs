using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enable this script when password is correct
public class RotateLightFrame : MonoBehaviour
{
    float rotateSpeed = 3f;
    void FixedUpdate(){
        this.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);        
    }
}
