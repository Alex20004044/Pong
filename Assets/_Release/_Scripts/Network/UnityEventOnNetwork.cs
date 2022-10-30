using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Pong
{
    public class UnityEventOnNetwork : NetworkBehaviour
    {
        [SerializeField]
        UnityEvent onEvent;

        [SerializeField]
        ActivationCondition activationCondition = ActivationCondition.always;
        public override void OnNetworkSpawn()
        {
            if (this.isActiveAndEnabled)
            {
                switch (activationCondition)
                {
                    case ActivationCondition.always:
                        onEvent.Invoke();
                        break;
                    case ActivationCondition.isClient:
                        if(IsClient) onEvent.Invoke();
                        break;
                    case ActivationCondition.isServer:
                        if(IsServer) onEvent.Invoke();
                        break;
                    case ActivationCondition.isHost:
                        if(IsHost) onEvent.Invoke();
                        break;
                    case ActivationCondition.isOwner:
                        if(IsOwner) onEvent.Invoke();
                        break;
                    case ActivationCondition.isNotOwner:
                        if(!IsOwner) onEvent.Invoke();
                        break;
                }
            }
        }
        private void Start()
        {
            //Need for enable/disable checkbox
        }

        enum ActivationCondition { always, isClient, isServer, isHost, isOwner, isNotOwner};
    }
}