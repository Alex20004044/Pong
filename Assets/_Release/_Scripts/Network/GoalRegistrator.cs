using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    [System.Serializable]
    public class GoalRegistrator : NetworkBehaviour
    {
/*        /// <summary>
        /// Invoked when player won in round
        /// </summary>
        public event Action<PlayerEnum> GoalRegisteredEvent;*/
        bool[] registeredGoals = new bool[2];
        Action<PlayerEnum> onGoalCallback;

        bool isWork = false;
        //PlayerEnum wonPlayer;
        public override void OnNetworkSpawn()
        {

        }

        public void StartRegistration(Action<PlayerEnum> onGoalCallback)
        {
            if (!IsServer) return;
            isWork = true;
            this.onGoalCallback = onGoalCallback;
            ResetState();
        }
        void ResetState()
        {
            for (int i = 0; i < registeredGoals.Length; i++)
            {
                registeredGoals[i] = false;
            }
        }

        public void RegisterGoal(PlayerEnum wonPlayer)
        {
            RegisterGoalServerRPC(NetworkManager.Singleton.LocalClientId, wonPlayer);
            Debug.Log("Register goal on player: " + NetworkManager.Singleton.LocalClientId);
        }

        [ServerRpc(RequireOwnership = false)]
        void RegisterGoalServerRPC(ulong playerId, PlayerEnum wonPlayer)
        {
            registeredGoals[playerId] = true;
            OnGoalRegistered(wonPlayer);
        }
        void OnGoalRegistered(PlayerEnum wonPlayer)
        {
            if (!isWork) return;

            for (int i = 0; i < registeredGoals.Length; i++)
            {
                Debug.Log(i + " " + registeredGoals[i]);
            }

            if (Array.TrueForAll(registeredGoals, (x)=> x))
            {
                isWork = false;
                onGoalCallback.Invoke(wonPlayer);
                InformThatGoalRegisteredClientRPC(wonPlayer);
            }
        }
        [ClientRpc]
        void InformThatGoalRegisteredClientRPC(PlayerEnum wonPlayer)
        {
            Debug.Log("REGISTERED GOAL ON EVERY MACHINE!");
        }

    }
}