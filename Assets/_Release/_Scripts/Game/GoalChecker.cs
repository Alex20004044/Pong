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
        float deltaRegistrationFromYBorder = 0.5f;
        float mapHeight;

        Transform ball;
        private void Awake()
        {
            Messenger.Subscribe(GameEventsPong.I_ROUND_STARTED, OnRoundStarted).AddTo(this);
        }
        private void Start()
        {
            //Attention!
            mapHeight = FindObjectOfType<SceneController>().GetYAxisMaxCoord().y - deltaRegistrationFromYBorder;
        }
        //TEST!!!
        void OnRoundStarted()
        {
            ball = FindObjectOfType<BallMovement>().transform;
        }
        private void FixedUpdate()
        {
            if (ball == null) return;
            if (ball.position.y > mapHeight)
                BallInTopGate();
            if (ball.position.y < -mapHeight)
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