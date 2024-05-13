using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerCurtainCollider : MonoBehaviour
{
    BoxCollider boxCollider;
    Vector3 startCenter =new Vector3(1.81f, 0.842737f, 4);
    Vector3 startSize =new Vector3(2.27f, 2.332798f,3.34f);
    Vector3 endCenter =new Vector3(1.5f, 0.842737f, 2.41f);
    Vector3 endSize =new Vector3(1.4f, 2.332798f, 0.18f);
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
        boxCollider.center = startCenter;
        boxCollider.size = startSize;
    }

    public IEnumerator ShrinkCollider(){
        float defaultDuration = 2f;
        float passedTime = 0f;
        while(passedTime < defaultDuration){
            passedTime += Time.deltaTime;
            boxCollider.center = Vector3.Lerp(startCenter, endCenter, passedTime / defaultDuration);
            boxCollider.size = Vector3.Lerp(startSize, endSize, passedTime / defaultDuration);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        boxCollider.center = endCenter;
        boxCollider.size = endSize;
    }
}
