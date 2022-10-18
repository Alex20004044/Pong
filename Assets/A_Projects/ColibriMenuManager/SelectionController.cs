using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CorD.ColibriMenuManager;

[ExecuteInEditMode]
public class SelectionController : MonoBehaviour
{
    Transform parent;
    int siblingIndex;

    Transform onSelectParent;

    Transform currentSelectionWindow;

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        Selection.selectionChanged += OnSelectionChanged;
    }
    private void OnDisable()
    {
        Debug.Log("OnDisable");
        Selection.selectionChanged -= OnSelectionChanged;
    }

    void OnSelectionChanged()
    {
        if (currentSelectionWindow != null)
        {
            Unfocus(currentSelectionWindow);
            //currentSelectionWindow.SetParent(parent);
            //currentSelectionWindow.SetSiblingIndex(siblingIndex);
        }

        if (Selection.activeTransform == null) return;

        Window w = Selection.activeTransform.GetComponentInParent<Window>(true);
        if (w == null) return;

        Debug.Log("current window name: " + w.name);
        currentSelectionWindow = w.transform;
        Focus(currentSelectionWindow);
        //parent = currentSelectionWindow.parent;
        //siblingIndex = currentSelectionWindow.GetSiblingIndex();
        //
        //onSelectParent = currentSelectionWindow.GetComponentInParent<WindowController>().transform.GetChild(0);
        //currentSelectionWindow.SetParent(onSelectParent);
        //currentSelectionWindow.SetAsLastSibling();
    }

    private void Focus(Transform window)
    {
        SceneVisibilityManager.instance.Hide(window.parent.gameObject, true);
        SceneVisibilityManager.instance.Show(window.gameObject, true);
    }
    void Unfocus(Transform window)
    {
        SceneVisibilityManager.instance.Hide(window.parent.gameObject, true);
    }
}
