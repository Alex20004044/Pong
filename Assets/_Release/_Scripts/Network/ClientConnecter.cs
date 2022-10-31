using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Net;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using MSFD;

namespace Pong
{
    public class ClientConnecter : MonoBehaviour
    {
        [SerializeField]
        TMP_InputField inputField;
        [SerializeField]
        string incorrectIpText = "Incorrect ip!";
        public void Activate()
        {
            IPAddress address;
            if(!IPAddress.TryParse(inputField.text, out address))
            {
                inputField.text = incorrectIpText;
                return;
            }
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(address.ToString(), IPManager.Instance.GetPort());
            Messenger.Broadcast(NetworkEventsPong.R_START_CLIENT);
        }
    }
}