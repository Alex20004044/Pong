using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.UI
{
    public class WindowManagerDefault : MonoBehaviour, IWindowManager
    {
        public void Open(Window _window)
        {
            _window.EnableWindow();
        }
        public void Close(Window _window)
        {
            _window.DisableWindow();
        }
        public void InstantClose(Window _window)
        {
            Close(_window);
        }
    }
}