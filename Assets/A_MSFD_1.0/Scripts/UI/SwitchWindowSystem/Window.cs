using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD.UI
{
    public class Window : MonoBehaviour
    {
        [Header("Window must be active onStart to register self. Then it automatically disable")]
        public string windowName;

        [Header("If false => bakButtonEvent would be ignored")]
        public bool isBackButtonWork = true;

        [Header("Window which will open on BackButtonEvent. By default wil be opened previous window")]
        public string backWindowName;

        [Header("If true => previous window won't be closed")]
        public bool isPopUpWindow = false;

        [FoldoutGroup("Events")]
        public UnityEvent onWindowEnable;
        [FoldoutGroup("Events")]
        public UnityEvent onWindowDisable;

        WindowController windowController;
        Canvas canvas;

        private void Start()
        {
            if (string.IsNullOrEmpty(windowName))
            {
                Debug.LogError("You should name Window: " + name);
                windowName = name.Substring(0, name.IndexOf("_"));
            }
            canvas = GetComponent<Canvas>();
            windowController = transform.parent.parent.parent.GetComponentInChildren<WindowController>();
            windowController.RegisterWindow(this);
        }
        public void EnableWindow()
        {
            if (canvas.enabled)
                return;
            else
            {
                canvas.enabled = true;
                onWindowEnable.Invoke();
#if UNITY_EDITOR
                name += GetPostfix();
#endif
            }
        }
        public void DisableWindow()
        {
            if (!canvas.enabled)
                return;
            else
            {
                canvas.enabled = false;
                onWindowDisable.Invoke();
#if UNITY_EDITOR
                name = name.Replace(GetPostfix(), "");
#endif
            }
        }
#if UNITY_EDITOR
        string GetPostfix()
        {
            if (isPopUpWindow)
                return " ^";
            else
                return " #";
        }
#endif
    }
}