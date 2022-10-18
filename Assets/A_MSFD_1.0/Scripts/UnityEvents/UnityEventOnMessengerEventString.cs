using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnMessengerEventString : UnityEventBase
    {
        [MessengerEvent]
        [Header("Activate unity Event on event if script is active")]
        [SerializeField]
        string eventName;
        [SerializeField]
        string value;

        private void Awake()
        {
            Messenger<string>.AddListener(eventName, OnRecieveEvent);
        }
        private void OnDestroy()
        {
            Messenger<string>.RemoveListener(eventName, OnRecieveEvent);
        }
        public void OnRecieveEvent(string arg)
        {
            if (arg == value)
            {
                OnEvent();
            }
        }
    }
}