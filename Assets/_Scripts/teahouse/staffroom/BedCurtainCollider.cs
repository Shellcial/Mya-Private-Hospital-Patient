using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedCurtainCollider : MonoBehaviour
{
    BoxCollider boxCollider;
    Vector3 startCenter =new Vector3(3.52942f, 0.8249514f, -1.341146f);
    Vector3 startSize =new Vector3(2.881981f, 2.285988f,0.0716238f);
    Vector3 endCenter =new Vector3(2.395806f, 0.8249514f, -1.341146f);
    Vector3 endSize =new Vector3(0.7173736f, 2.285988f, 0.07162388f);

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
