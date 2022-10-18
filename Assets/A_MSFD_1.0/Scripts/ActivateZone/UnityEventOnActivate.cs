using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnActivate : UnityEventBase, IActivator
    {
        [SerializeField]
        ActivateMode activateMode = ActivateMode.once;

        bool isWasActivated = false;
        public bool TryActivate()
        {
            if (activateMode == ActivateMode.once && isWasActivated)
            {
                return false;
            }
            else
            {
                isWasActivated = true;
                OnEvent();
                return true;
            }
        }
        /// <summary>
        /// This method can allow activate repeatedly in once activate mode
        /// </summary>
        public void AllowActivation()
        {
            isWasActivated = false;
        }
        public void ChangeActivateMode(ActivateMode _activateMode)
        {
            activateMode = _activateMode;
        }
        public enum ActivateMode { once, multiple};
    }
}