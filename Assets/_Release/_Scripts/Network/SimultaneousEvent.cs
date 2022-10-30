using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Pong
{
    public class SimultaneousEvent : NetworkBehaviour
    {
        [SerializeField]
        UnityEvent syncEvent;

        [Button]
        public void ActivateSyncedEvent()
        {
            if(IsServer)
            {
                InvokeSyncedEventClientRPC();
                StartCoroutine(InvokeSyncedEventOnServer((NetworkManager.Singleton.LocalTime - NetworkManager.Singleton.ServerTime).TimeAsFloat));
            }
        }
        [ClientRpc]
        void InvokeSyncedEventClientRPC()
        {
            if (IsServer) return;
            syncEvent.Invoke();
        }
        IEnumerator InvokeSyncedEventOnServer(float timeToWait)
        {
            if (timeToWait > 0)
            {
                yield return new WaitForSeconds(timeToWait);
            }
            syncEvent.Invoke();
        }
    }
}