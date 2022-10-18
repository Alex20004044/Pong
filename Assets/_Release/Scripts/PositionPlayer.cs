using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PositionPlayer : NetworkBehaviour
{
    [SerializeField]
    Vector3 firstPlayerPos = new Vector3(0, -5, 0);    
    [SerializeField]
    Vector3 secondPlayerPos = new Vector3(0, 5, 0);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsHost)
        {
            transform.position = firstPlayerPos;
        }
        else if (IsClient)
        {
            transform.position = secondPlayerPos;
        }
        else
            Debug.LogError("Non client or host");
    }
}
