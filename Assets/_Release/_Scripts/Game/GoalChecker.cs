using MSFD;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Pong
{
    public class GoalChecker : MonoBehaviour
    {
        [SerializeField]
        GoalRegistrator goalRegistrator;
        [SerializeField]
        float deltaFromYBorderForRegistration = 0.5f;

        float mapBorder;
        Transform ball;
        private void Awake()
        {
            Messenger.Subscribe(GameEventsPong.I_ROUND_STARTED, OnRoundStarted).AddTo(this);
        }
        //TEST!!!
        void OnRoundStarted()
        {
            mapBorder = FindObjectOfType<SceneController>().GetYAxisMaxCoord().y - deltaFromYBorderForRegistration;
            ball = GameObject.FindGameObjectWithTag(GameValuesPong.ballTag).transform;
        }
        private void FixedUpdate()
        {
            if (ball == null) return;
            if (ball.position.y > mapBorder)
                BallInTopGate();
            if (ball.position.y < -mapBorder)
                BallInDownGate();
        }

        void BallInTopGate()
        {
            goalRegistrator.RegisterGoal(PlayerEnum.playerOne);
        }
        void BallInDownGate()
        {
            goalRegistrator.RegisterGoal(PlayerEnum.playerTwo);
        }
    }
}