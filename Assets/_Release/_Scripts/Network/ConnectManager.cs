using MSFD;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class ConnectManager : NetworkBehaviour
    {
        [SerializeField]
        string playScene = "Game";
        [SerializeField]
        string menuScene = "Menu";

        private void Awake()
        {
            Messenger.Subscribe(NetworkEventsPong.R_START_HOST, StartHost).AddTo(this);
            Messenger.Subscribe(NetworkEventsPong.R_START_CLIENT, StartClient).AddTo(this);

            Messenger.Subscribe(NetworkEventsPong.R_CANCEL_CLIENT_CONNECTION, CancelClientConnection).AddTo(this);
            Messenger.Subscribe(NetworkEventsPong.R_DISCONNECT_FROM_ROOM, DisconnectFromRoom).AddTo(this);
        }

        public override void OnNetworkSpawn()
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += OnSomeClientDisconnect;
        }
        public override void OnNetworkDespawn()
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnSomeClientDisconnect;
        }
        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
            NetworkManager.SceneManager.LoadScene(playScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }

        public void CancelClientConnection()
        {
            Debug.Log("Shutdown on client connection");
            NetworkManager.Singleton.Shutdown();
        }

        private void OnSomeClientDisconnect(ulong id)
        {
            DisconnectFromRoom();
        }

        public void DisconnectFromRoom()
        {
            NetworkManager.Singleton.Shutdown();
            LevelLoadManager.Instance.LoadLevel(menuScene);
        }
    }
}