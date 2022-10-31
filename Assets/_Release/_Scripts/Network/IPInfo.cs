using MSFD;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
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

        private void Awake()
        {
            Messenger.Subscribe(GameEvents.I_GAME_AWAKED, OnGameAwaked).AddTo(this);
        }

        public override void OnNetworkSpawn()
        {
            if (IsHost)
                text.text = ipInfoTextPrefix + GetConnectionInfo();
            else
                gameObject.SetActive(false);

        }
        void OnGameAwaked()
        {
            gameObject.SetActive(false);
        }


        string GetConnectionInfo()
        {
            return NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address;
        }
    }
}