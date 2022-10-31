using MSFD;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UniRx;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using static Unity.Netcode.Transports.UTP.UnityTransport;

namespace Pong
{
    public class IPManager : SingletoneBase<IPManager>
    {
        [SerializeField]
        ushort port = 7777;

        [ReadOnly]
        [ShowInInspector]
        string myAddressLocal;
        [ReadOnly]
        [ShowInInspector]
        string myAddressGlobal;        

        [SerializeField]
        NetworkEventsPong.MultiplayerMode multiplayerMode = NetworkEventsPong.MultiplayerMode.local;

        private void Start()
        {
            #region GET IP
            //Get the local IP
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in hostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myAddressLocal = ip.ToString();
                    break;
                } //if
            } //foreach
              //Get the global IP
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.ipify.org");
            request.Method = "GET";
            request.Timeout = 1000; //time in ms
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    myAddressGlobal = reader.ReadToEnd();
                } //if
                else
                {
                    Debug.LogError("Timed out? " + response.StatusDescription);
                    myAddressGlobal = "127.0.0.1";
                } //else
            } //try
            catch (WebException ex)
            {
                Debug.Log("Likely no internet connection: " + ex.Message);
                myAddressGlobal = "127.0.0.1";
            } //catch

            #endregion
            SetMultiplayerMode(NetworkEventsPong.MultiplayerMode.local);
            Messenger<NetworkEventsPong.MultiplayerMode>.
                Subscribe(NetworkEventsPong.R_MultiplayerMode_SET_MULTIPLAYER_MODE, SetMultiplayerMode).AddTo(this);

        }

        
        public void SetMultiplayerMode(NetworkEventsPong.MultiplayerMode mode)
        {
            multiplayerMode = mode;
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            if (mode == NetworkEventsPong.MultiplayerMode.local)
                transport.SetConnectionData(myAddressLocal, port, "0.0.0.0");
            else
                transport.SetConnectionData(myAddressGlobal, port, myAddressGlobal);

            Debug.Log("Current multiplayer mode: " + multiplayerMode.ToString());
        }
        public ushort GetPort()
        {
            return port;
        }
    }
}