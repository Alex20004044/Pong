using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CorD.ColibriMenuManager
{
    public class Window : MonoBehaviour
    {
        [SerializeField]
        string windowName;

        [SerializeField]
        bool isBackButtonWork = true;

        [ShowIf(nameof(isBackButtonWork))]
        [SerializeField]
        string backWindowName;

        [SerializeField]
        [Header("If true => previous window won't be closed")]
        bool isPopUpWindow = false;

        public void Initialize()
        {

        }

        public string GetWindowName()
        {
            return windowName;
        }
        public bool IsBackButtonWork()
        {
            return isBackButtonWork;
        }
        public string GetBackWindowName()
        {
            return backWindowName;
        }
        public bool IsPopUpWindow()
        {
            return isPopUpWindow;
        }


    }
}