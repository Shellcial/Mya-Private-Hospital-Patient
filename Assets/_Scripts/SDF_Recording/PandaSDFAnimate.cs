using UnityEngine;
using UnityEngine.VFX;

public class PandaSDFAnimate : MonoBehaviour
{
    [SerializeField]
    private float speedVariable = 1f;
    [SerializeField]
    private bool isUpdateFade;
    [SerializeField]
    private bool isFade;
    [SerializeField]
    private bool isRotate;
    [SerializeField]
    private float fadeSpeedVariable = 0.02f;
    private float startFade = -0.5f;
    private float currentFade;
    private float targetFade = 0.5f;
    private float targetDrag = 0.1f;
    private float currentDrag = 3f;
    [SerializeField]
    private VisualEffect vfx;

    void Start(){
        currentFade = startFade;
    }

    void FixedUpdate()
    {
        if (isUpdateFade){
            if (isFade){
                currentFade += 0.002f;
                if (currentFade >= targetFade){
                    currentFade = targetFade;
                }
                vfx.SetFloat("heightCompare", currentFade);
                if (currentFade >= targetFade * 0.5f){
                    currentDrag -= 0.02f;
                    if (currentDrag <= targetDrag){
                        currentDrag = targetDrag;
                    }
                    vfx.SetFloat("dragPower", currentDrag);
                }
            }
            else {
                currentFade -= fadeSpeedVariable * Time.deltaTime;
                if (currentFade <= startFade){
                    currentFade = startFade;
                }

                vfx.SetFloat("heightCompare", currentFade);
            }
        }

        
        if (isRotate){
            transform.Rotate(Vector3.up * speedVariable * Time.deltaTime);
        }
    }
}
