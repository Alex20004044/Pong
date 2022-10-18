using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Linq;

namespace MSFD.UI
{
    [RequireComponent(typeof(IWindowManager))]
    public class WindowController : MonoBehaviour
    {
        [Header("Window, which will be opened on start")]
        [SerializeField]
        string startWindowName = "Menu";
        [SerializeField]
        bool isEscapeButtonWork = true;

        IWindowManager manageWindow;//Script, which changes windows
        Dictionary<string, Window> windows = new Dictionary<string, Window>();
        [ReadOnly]
        [ShowInInspector]
        Stack<Window> windowStack = new Stack<Window>();

        private void Awake()
        {
            Messenger<string>.AddListener(UIEvents.R_string_OPEN_WINDOW, OnOpenWindow);
            manageWindow = GetComponent<IWindowManager>();
            if (manageWindow == null)
            {
                manageWindow = gameObject.AddComponent<WindowManagerDefault>();
            }

            if (string.IsNullOrEmpty(startWindowName))
            {
                Debug.LogError("Start window is not set");
            }
        }
        private void OnDestroy()
        {
            Messenger<string>.RemoveListener(UIEvents.R_string_OPEN_WINDOW, OnOpenWindow);
        }

        private void Update()
        {
            if (isEscapeButtonWork && Input.GetKeyDown(KeyCode.Escape))
            {
                BackButtonEvent();
            }
        }
        public void RegisterWindow(Window window)
        {
            Window w;
            if (windows.TryGetValue(window.windowName, out w))
            {
                Debug.LogError("Try to add already existing window");
                return;
            }
            windows.Add(window.windowName, window);

            //window.DisableWindow();

            if (window.windowName == startWindowName)
            {
                windowStack.Push(window);
                manageWindow.Open(window);
                //window.EnableWindow();
            }
            else
            {
                InstantClose(window);
            }
        }
        void InstantClose(Window _window)
        {
            manageWindow.InstantClose(_window);
        }
        public void UnregisterWindow(Window window)
        {
            InstantClose(window);

            List<Window> stackList = windowStack.ToList();
            stackList.RemoveAll((x) => x.Equals(window));
            windowStack = new Stack<Window>(stackList);

            windows.Remove(window.name);
            
        }
        public void OnOpenWindow(string _windowName)
        {
            OpenWindow(_windowName);
        }
        void OpenWindow(string _windowName)
        {
            if (string.IsNullOrEmpty(_windowName))
            {
                BackButtonEvent();
            }
            else if (_windowName == windowStack.Peek().windowName)
            {
                Debug.LogError("Attempt to open already opened window");
                return;
            }
            else
            {
                Window openableWindow;
                if (windows.TryGetValue(_windowName, out openableWindow))
                {
                    if (openableWindow.isPopUpWindow)
                    {
                        windowStack.Push(openableWindow);
                        manageWindow.Open(openableWindow);
                    }
                    else
                    {
                        //Close pop up windows
                        while (windowStack.Peek().isPopUpWindow)
                        {
                            var popW = windowStack.Pop();
                            manageWindow.Close(popW);
                            Messenger<string>.Broadcast(UIEvents.I_string_WINDOW_CLOSED, popW.windowName, MessengerMode.DONT_REQUIRE_LISTENER);
                        }
                        var w = windowStack.Peek();
                        manageWindow.Close(w);
                        Messenger<string>.Broadcast(UIEvents.I_string_WINDOW_CLOSED, w.windowName, MessengerMode.DONT_REQUIRE_LISTENER);
                        //
                        if (!openableWindow.isBackButtonWork || !string.IsNullOrEmpty(openableWindow.backWindowName))
                        {
                            windowStack.Clear();
                        }
                        windowStack.Push(openableWindow);
                        manageWindow.Open(openableWindow);
                    }
                    Messenger<string>.Broadcast(UIEvents.I_string_WINDOW_OPENED, _windowName, MessengerMode.DONT_REQUIRE_LISTENER);
                }
                else
                {
                    Debug.LogError("Try to open unknown window: " + _windowName);
                }
            }
        }
        public void BackButtonEvent()
        {
            Window window = windowStack.Peek();
            if (!window.isBackButtonWork)
            {
                return;
            }

            if (string.IsNullOrEmpty(window.backWindowName))
            {
                if (windowStack.Count <= 1)
                {
                    Debug.LogError("Attempt to close startWindow");
                    if (windowStack.Count == 0)
                    {
                        Debug.LogError("Attempt to activate BackButton on empty windowStack");
                    }
                }
                else
                {
                    manageWindow.Close(window);

                    windowStack.Pop();
                    manageWindow.Open(windowStack.Peek());
                }
            }
            else
            {
                OpenWindow(window.backWindowName);
            }
            Messenger.Broadcast(UIEvents.I_BACK_BUTTON_PRESSED, MessengerMode.DONT_REQUIRE_LISTENER);
        }
    }
}