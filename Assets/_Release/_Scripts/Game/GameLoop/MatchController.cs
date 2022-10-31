using MSFD;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class MatchController : NetworkBehaviour
    {
        [SerializeField]
        int scoresForWin = 10;

        [ReadOnly]
        [ShowInInspector]
        GameState state = GameState.waitingForPlayers;

        [SerializeField]
        float delayBeforeStart = 3f;
        [SerializeField]
        float delayAfterRoundEnd = 1f;


        [SerializeField]
        DelayBeforeMatchController delayBeforeMatchController;

        [SerializeField]
        RoundController roundController;
        [SerializeField]
        ScoreController scoreController;
        [SerializeField]
        AfterMatchController afterMatchController;

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                enabled = false;
                return;
            }
            Messenger.Subscribe(GameEvents.R_BEGIN_GAME, StartGame).AddTo(this);
        }

        [Button]
        public void StartGame()
        {
            state = GameState.delayBeforeStart;
            Messenger.Broadcast(GameEvents.I_GAME_AWAKED, MessengerMode.DONT_REQUIRE_LISTENER);
            scoreController.Initialize();

            delayBeforeMatchController.StartCountdown(delayBeforeStart, OnBeforeStartCountdownEnd);
        }

        void OnBeforeStartCountdownEnd()
        {
            Messenger.Broadcast(GameEvents.I_GAME_STARTED, MessengerMode.DONT_REQUIRE_LISTENER);
            state = GameState.playRound;
            roundController.StartRound(OnRoundEnd);
        }

        void OnRoundEnd(PlayerEnum wonPlayer)
        {
            scoreController.AddScore(wonPlayer);
            StartCoroutine(OnRoundEndCoroutine(wonPlayer));
        }
        IEnumerator OnRoundEndCoroutine(PlayerEnum wonPlayer)
        {
            yield return new WaitForSeconds(delayAfterRoundEnd);
            //Start new round or end game
            if (scoreController.GetScore(wonPlayer) >= scoresForWin)
            {
                EndGame(wonPlayer);
            }
            else
                roundController.StartRound(OnRoundEnd);
        }

        [Button]
        public void EndGame(PlayerEnum wonPlayer)
        {
            Messenger.Broadcast(GameEvents.I_GAME_ENDED, MessengerMode.DONT_REQUIRE_LISTENER);
            afterMatchController.EndGame(wonPlayer);

            Debug.Log("end game");
        }


        enum GameState { waitingForPlayers, delayBeforeStart, playRound, endGame };
    }
}