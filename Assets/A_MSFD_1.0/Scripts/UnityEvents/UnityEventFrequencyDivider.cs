using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace MSFD
{
    public class UnityEventFrequencyDivider : UnityEventBase
    {
        [SerializeField]
        int recievedEventsCount = 2;

        int currentEventsRecieved = 0;
        public void RecieveEvent(int value = 1)
        {
            currentEventsRecieved += value;
            if(currentEventsRecieved >= recievedEventsCount)
            {
                currentEventsRecieved %= recievedEventsCount;
                OnEvent();
            }
        }
    }
}
