using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class PositionPlayer : NetworkBehaviour
    {
        [SerializeField]
        Vector3[] playerPositions = new Vector3[2];

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
                transform.position = playerPositions[NetworkManager.Singleton.LocalClientId];
        }
    }
}