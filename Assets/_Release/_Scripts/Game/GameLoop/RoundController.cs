using MSFD;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class RoundController : NetworkBehaviour
    {
        [SerializeField]
        float delayBeforeBallActivation = 2f;
        [SerializeField]
        GameObject ballPrefab;
        [SerializeField]
        Transform ballSpawnPoint;
        [SerializeField]
        GoalRegistrator goalRegistrator;

        GameObject ball;
        NetworkObject ballNetworkObject;

        Action<PlayerEnum> onRoundEndCallback;

        public void StartRound(Action<PlayerEnum> onRoundEndCallback)
        {
            if (!IsServer) return;
            if (ballNetworkObject != null) ballNetworkObject.Despawn();

            this.onRoundEndCallback = onRoundEndCallback;
            //sapwnBall
            ball = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation);
            ballNetworkObject = ball.GetComponent<NetworkObject>();
            ballNetworkObject.Spawn();

            //wait//ActivateBall
            StartCoroutine(WaitAndActivateBall());

            goalRegistrator.StartRegistration(OnGoal);

            Messenger.Broadcast(GameEventsPong.I_ROUND_STARTED, MessengerMode.DONT_REQUIRE_LISTENER);
        }
        IEnumerator WaitAndActivateBall()
        {
            yield return new WaitForSeconds(delayBeforeBallActivation);
            ball.GetComponent<SimultaneousBallActivator>().ActivateSyncedEvent();
        }
        public void OnGoal(PlayerEnum wonPlayer)
        {
            ballNetworkObject.Despawn();
            onRoundEndCallback.Invoke(wonPlayer);
        }

    }
}