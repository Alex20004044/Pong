using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class WindowSelector : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Object was selected");
    }
    public void OnDeselect(BaseEventData eventData)
    {

        Debug.Log("Object was deselected");
    }
}
