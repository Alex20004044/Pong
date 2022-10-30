using InfinityCode.uContext.Tools;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class PingManager : NetworkBehaviour
    {
        [SerializeField]
        float updateRate = 0.1f;
        [SerializeField]
        float ping = -1;

        [SerializeField]
        bool isLogPing = false;
        public static PingManager Instance => _instance;
        static PingManager _instance;
        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                DestroyImmediate(gameObject);
        }

        public override void OnNetworkSpawn()
        {
            if (!IsHost)
                Observable.Interval(TimeSpan.FromSeconds(updateRate)).Subscribe((_) => UpdatePing()).AddTo(this);

        }

        void UpdatePing()
        {
            ping = (NetworkManager.Singleton.LocalTime - NetworkManager.Singleton.ServerTime).TimeAsFloat;
            if (isLogPing)
                Debug.Log("Ping: " + ping);
            UpdatePingServerRPC(ping);
        }
        [ServerRpc(RequireOwnership = false)]
        void UpdatePingServerRPC(float ping)
        {
            this.ping = ping;
            if (isLogPing)
                Debug.Log("Ping: " + ping);
        }

        public static float GetPing()
        {
            return Instance.ping;
        }

    }
}