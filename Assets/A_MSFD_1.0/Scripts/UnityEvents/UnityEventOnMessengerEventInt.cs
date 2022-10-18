using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace MSFD
{
    public class UnityEventOnMessengerEventInt : UnityEventBase
    {
        [MessengerEvent]
        [Header("Activate unity Event on event if script is active")]
        [SerializeField]
        string eventName;
        [SerializeField]
        int num;

        private void Awake()
        {
            Messenger<int>.AddListener(eventName, OnRecieveEvent);
        }
        private void OnDestroy()
        {
            Messenger<int>.RemoveListener(eventName, OnRecieveEvent);
        }
        public void OnRecieveEvent(int arg)
        {
            if (arg == num)
            {
                OnEvent();
            }
        }
    }
}