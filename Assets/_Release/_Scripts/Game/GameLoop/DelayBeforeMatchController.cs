using MSFD;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class DelayBeforeMatchController : NetworkBehaviour
    {   [SerializeField]
        float updateTimerDelay = 0.5f;
        [SerializeField]
        string startMessage = "Waiting for players";

        private void Start()
        {
            Messenger<string>.Broadcast(GameEventsPong.R_string_DISPLAY_MESSAGE, startMessage, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        public void StartCountdown(float time, Action onCountdownEndCallback)
        {
            StartCoroutine(CountForGameStart(time, onCountdownEndCallback));
        }

        IEnumerator CountForGameStart(float time, Action onCountdownEndCallback)
        {
            while (time > 0)
            {
                time -= updateTimerDelay;
                InformClientsClientRPC(Mathf.RoundToInt(time).ToString());
                
                yield return new WaitForSeconds(updateTimerDelay);
            }
            //Clean Display from messages
            InformClientsClientRPC("");
            onCountdownEndCallback();
        }

        [ClientRpc]
        void InformClientsClientRPC(string message)
        {
            Messenger<string>.Broadcast(GameEventsPong.R_string_DISPLAY_MESSAGE, message, MessengerMode.DONT_REQUIRE_LISTENER);
        }
    }
}