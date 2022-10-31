using MSFD.AS;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Pong
{
    public class SimultaneousBallActivator : NetworkBehaviour
    {

        [SerializeField]
        float minYComponent = 0.3f;

        Vector2 GetMoveDirection()
        {
            float x = Random.Range(0f, 1f);
            float y = Random.Range(minYComponent, 1f);

            int[] sings = { -1, 1 };

            x *= sings[Rand.GetRandomIndex(sings.Length)];
            y *= sings[Rand.GetRandomIndex(sings.Length)];

            Debug.Log("Rand Index (0,1): " + Rand.GetRandomIndex(sings.Length));
            return new Vector2(x, y).normalized;
        }

        [Button]
        public void ActivateSyncedEvent()
        {
            if (IsServer)
            {
                Vector2 moveDirection = GetMoveDirection();
                float deltaTime = (NetworkManager.Singleton.LocalTime - NetworkManager.Singleton.ServerTime).TimeAsFloat;
                Debug.Log("Delta between host and client: " + deltaTime);

                //InvokeSyncedEventServerRPC(deltaTime, moveDirection);
                //StartCoroutine(WaitAndActivateBall(0, moveDirection));                

                InvokeSyncedEventClientRPC(0, moveDirection);
                StartCoroutine(WaitAndActivateBall(deltaTime, moveDirection));
            }
            else
            {
                Vector2 moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                float deltaTime = (NetworkManager.Singleton.LocalTime - NetworkManager.Singleton.ServerTime).TimeAsFloat;
                Debug.Log("Delta between host and client: " + deltaTime);

                //InvokeSyncedEventServerRPC(deltaTime, moveDirection);
                //StartCoroutine(WaitAndActivateBall(0, moveDirection));                
                
                InvokeSyncedEventServerRPC(0, moveDirection);
                StartCoroutine(WaitAndActivateBall(deltaTime, moveDirection));
            }
        }
        [ClientRpc]
        void InvokeSyncedEventClientRPC(float timeToWait, Vector2 moveDirection)
        {
            if (IsServer) return;
            StartCoroutine(WaitAndActivateBall(timeToWait, moveDirection));
        }
        [ServerRpc(RequireOwnership = false)]
        void InvokeSyncedEventServerRPC(float timeToWait, Vector2 moveDirection)
        {
            StartCoroutine(WaitAndActivateBall(timeToWait, moveDirection));
        }
        IEnumerator WaitAndActivateBall(float timeToWait, Vector2 moveDirection)
        {
            if (timeToWait > 0)
            {
                yield return new WaitForSeconds(timeToWait);
            }
            GetComponent<IVector2Settable>().SetDirection(moveDirection);
        }
    }
}