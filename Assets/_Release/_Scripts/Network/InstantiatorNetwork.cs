using MSFD;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public static class InstantiatorNetwork
    {
        public static GameObject Create(GameObject prefab, Vector3 position = default, Quaternion rotation = default, bool isActive = true, Transform parent = null)
        {
            GameObject go = Object.Instantiate(prefab, position, rotation, parent);
            go.SetActive(isActive);
            NetworkObject networkObject = go.GetComponent<NetworkObject>();
            networkObject.Spawn(true);
            networkObject.TrySetParent(parent);
            return go;
        }
        public static void Destruct(GameObject go, float time = 0)
        {
            NetworkObject networkObject = go.GetComponent<NetworkObject>();
            networkObject.Despawn();
        }
    }
}