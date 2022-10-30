using MSFD;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class GameStarter : NetworkBehaviour
    {


        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                enabled = false;
                return;
            }

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        }
        public override void OnNetworkDespawn()
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
        }

        private void OnClientConnectedCallback(ulong obj)
        {
            Messenger.Broadcast(GameEvents.R_BEGIN_GAME);
        }
    }
}