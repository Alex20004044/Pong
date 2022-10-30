using MSFD;
using Sirenix.OdinInspector;
using System;
using UniRx;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class ScoreController : NetworkBehaviour
    {
        [ReadOnly]
        [ShowInInspector]
        int[] playerScores = new int[2];

        public void Initialize()
        {
            if (!IsServer) return;

            for (int i = 0; i < playerScores.Length; i++)
            {
                playerScores[i] = 0;
            }
            OnScoreChanged();
        }

        [Button]
        public void AddScore(PlayerEnum player)
        {
            if (!IsServer) return;

            playerScores[(int)player]++;
            OnScoreChanged();
        }

        public int GetScore(PlayerEnum player)
        {
            return playerScores[(int)player];
        }
        void OnScoreChanged()
        {
            UpdateScoresClientRPC(playerScores[0], playerScores[1]);
        }
        [ClientRpc]
        void UpdateScoresClientRPC(int scoreOne, int scoreTwo)
        {
            playerScores[0] = scoreOne;
            playerScores[1] = scoreTwo;
            Messenger<string>.Broadcast(GameEventsPong.I_string_SCOREBOARD, GetScoreBoard());
        }
        string GetScoreBoard()
        {
            return playerScores[0] + " | " + playerScores[1];
        }
    }
}