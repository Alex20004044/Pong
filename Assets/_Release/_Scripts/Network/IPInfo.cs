using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Pong
{
    public class IPInfo : NetworkBehaviour
    {
        [SerializeField]
        string ipInfoTextPrefix = "Your IP is: ";

        [SerializeField]
        TMP_Text text;


        public override void OnNetworkSpawn()
        {
            if (IsHost)
                text.text = ipInfoTextPrefix + GetConnectionInfo();
            else
                enabled = false;

        }

        string GetConnectionInfo()
        {
            return NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address;
        }
    }
}