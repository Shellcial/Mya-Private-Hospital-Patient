using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reset the two adjustable parameters during runtime, since scriptable object will remain the change of result during runtime
public class ResetScriptableObject : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<InteractableObject>() != null)
        {
            //reset scriptable object (for testing stage / final stage)
            InteractableObject targetStatus = GetComponent<InteractableObject>();
            if (!targetStatus.interactableStatus.isInteractable)
            {
                targetStatus.interactableStatus.isInteractable = true;
            }
            if (!targetStatus.interactableStatus.isHighlightable)
            {
                targetStatus.interactableStatus.isHighlightable = true;
            }
        }
    }
}
