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
        Action<PlayerEnum> onGoalCallback;

        bool isWork = false;

        public void StartRegistration(Action<PlayerEnum> onGoalCallback)
        {
            if (!IsServer) return;
            isWork = true;
            this.onGoalCallback = onGoalCallback;
        }
        public void RegisterGoal(PlayerEnum wonPlayer)
        {
            OnGoalRegistered(wonPlayer);
        }
        void OnGoalRegistered(PlayerEnum wonPlayer)
        {
            if (!isWork) return;
            isWork = false;
            onGoalCallback.Invoke(wonPlayer);
        }

    }
}