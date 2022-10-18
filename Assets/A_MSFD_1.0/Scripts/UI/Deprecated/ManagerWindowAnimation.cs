using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.UI
{
    public class ManagerWindowAnimation : MonoBehaviour, IWindowManager
    {
        [Header("You should manually set canvas.enabled = false in close animation")]
        [SerializeField]
        string closeWindowAnimationName = "CloseWindow_animation";
        [Header("You should manually set canvas.enabled =true in open animation")]
        [SerializeField]
        string openWindowAnimationName = "OpenWindow_animation";
        public void Close(Window _window)
        {
            var animation = _window.GetComponent<Animation>();
            if (!animation.Play(closeWindowAnimationName))
            {
                Debug.LogError("Close window animation error");
            }
            //animation.GetClip(closeWindowAnimationName).
        }

        public void InstantClose(Window _window)
        {
            Close(_window);
        }

        public void Open(Window _window)
        {
            if (!_window.GetComponent<Animation>().Play(openWindowAnimationName))
            {
                Debug.LogError("Open window animation error");
            }
        }
    }
}