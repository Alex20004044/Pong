using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class ConnectManager : NetworkBehaviour
    {
        [SerializeField]
        string playScene = "Game";
        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
            NetworkManager.SceneManager.LoadScene(playScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }        
        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }



        public void DisconnectFromRoom()
        {
            if(IsHost)
            {
                //NetworkManager.Singleton.D
            }
            else
            {

            }
        }
    }
}