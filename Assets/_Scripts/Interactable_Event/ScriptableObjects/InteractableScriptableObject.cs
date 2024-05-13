using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class allows the interactable object to switch its interactable stage

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InteractableObject", order = 1)]
public class InteractableScriptableObject : ScriptableObject
{
    public bool isInteractable;
    public bool isHighlightable;
    public bool isChildHighlight;
    public bool isAllChild;
    public List<string> excludedChild;
}
