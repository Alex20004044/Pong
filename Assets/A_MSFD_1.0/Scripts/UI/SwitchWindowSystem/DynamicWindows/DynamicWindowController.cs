using MSFD.UI;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicWindowController : MonoBehaviour
{
    [SerializeField]
    WindowController windowController;
    [SerializeField]
    Transform dynamicWindowParent;

    [SerializeField]
    GameObject dynamicWindowPrefab;

    [Button]
    public void OpenDynamicWindow(string header, string text)
    {
        GameObject  dynamicWindowGo = PC.Spawn(dynamicWindowPrefab, Vector3.zero, Quaternion.identity, false, dynamicWindowParent);
        Window dynamicWindow = dynamicWindowGo.GetComponent<Window>();
        dynamicWindow.windowName = header;

        windowController.RegisterWindow(dynamicWindow);
        windowController.OnOpenWindow(header);

        dynamicWindowGo.GetComponentInChildren<TMP_Text>().text = text;

        dynamicWindow.onWindowDisable.AddListener(() => OnDynamicWindowDisable(dynamicWindow));
    }

    void OnDynamicWindowDisable(Window dynamicWindow)
    {
        windowController.UnregisterWindow(dynamicWindow);
        dynamicWindow.onWindowDisable.RemoveListener(()=>OnDynamicWindowDisable(dynamicWindow));
        PC.Despawn(dynamicWindow.gameObject);
    }
}
