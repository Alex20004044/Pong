using MSFD;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CorD.ColibriMenuManager
{
    public class WindowController : MonoBehaviour
    {
        [Header("Window, which will be opened on start")]
        [SerializeField]
        string startWindowName = "Menu";
        [SerializeField]
        bool isEscapeButtonWork = true;
        [SerializeField]
        bool isCloseAlreadyOpenedWindowIfItOnTopOfStack = false;

        Dictionary<string, Window> windows = new Dictionary<string, Window>();
        [ReadOnly]
        [ShowInInspector]
        Stack<Window> windowStack = new Stack<Window>();

        private void Awake()
        {
            Messenger<string>.Subscribe(UIEvents.R_string_OPEN_WINDOW, OpenWindow).AddTo(this);
        }

        private void Start()
        {
            var windowsArray = GetComponentsInChildren<Window>();

            foreach(var x in windowsArray)
            {
                windows.Add(x.GetWindowName(), x);
                x.gameObject.SetActive(false);//Probably deactivate?
                x.Initialize();
            }
            var startWindow = windows[startWindowName];
            windowStack.Push(startWindow);
            ActivateWindow(startWindow);
            //OpenWindow(startWindowName);
        }
        private void Update()
        {
            if (isEscapeButtonWork && Input.GetKeyDown(KeyCode.Escape))
            {
                BackButton();
            }
        }

        [Button]
        public void OpenWindow(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                BackButton();
                return;
            }
            else if (name == windowStack.Peek().GetWindowName())
            {
                if (isCloseAlreadyOpenedWindowIfItOnTopOfStack)
                    Debug.LogError("Attempt to open already opened window");
                else
                    BackButton();
                return;
            }

            Window window;
            if(!windows.TryGetValue(name, out window))
            {
                Debug.LogError("Attempt to open unknown window: " + name);
                return;
            }
            else
            {
                if (window.IsPopUpWindow())
                {
                    if(windowStack.Contains(window))
                    {
                        Debug.LogError("Attempt to open already opened popup window");
                        return;
                    }

                    windowStack.Push(window);
                    ActivateWindow(window);
                }
                else
                {
                    CloseTopPopUps();
                    var previousWindow = windowStack.Peek();
                    DeactivateWindow(previousWindow);

                    if (!window.IsBackButtonWork() || !string.IsNullOrEmpty(window.GetBackWindowName()))
                    {
                        windowStack.Clear();
                    }
                    //Probably should check is there loop of windows to clear stack?
                    windowStack.Push(window);
                    ActivateWindow(window);
                }
            }
        }

        void ActivateWindow(Window window)
        {
            window.gameObject.SetActive(true);
        }
        void DeactivateWindow(Window window)
        {
            window.gameObject.SetActive(false);
        }

        public void BackButton()
        {
            if(windowStack.Count == 1)
            {
                Debug.LogError("Attempt to close startWindow");
                return;
            }

            Window window = windowStack.Peek();
            if(!window.IsBackButtonWork())
            {
                return;
            }

            string backWindowName = window.GetBackWindowName();
            if (string.IsNullOrWhiteSpace(backWindowName))
            {
                windowStack.Pop();
                DeactivateWindow(window);

                ActivateWindow(windowStack.Peek());
            }
            else
            {
                OpenWindow(backWindowName);
            }
        }

        public void CloseTopPopUps()
        {
            while (windowStack.Peek().IsPopUpWindow())
            {
                var popup = windowStack.Pop();
                DeactivateWindow(popup);
            }
        }
        public void CloseAll()
        {
            int closeCount = windowStack.Count - 1;
            for(int i = 0; i < closeCount; i++)
            { 
                var popup = windowStack.Pop();
                DeactivateWindow(popup);
            }
            ActivateWindow(windowStack.Peek());
        }
    }
}