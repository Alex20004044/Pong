using MSFD;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Pong
{
    public class GoalController : MonoBehaviour
    {
        [SerializeField]
        GoalRegistrator goalRegistrator;
/*        [SerializeField]
        float mapHeight = 6;

        Transform ball;*/
        /*        private void Awake()
                {
                    Messenger.Subscribe(GameEventsPong.I_ROUND_STARTED, OnRoundStarted).AddTo(this);
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

                }*/

        void Awake()
        {
            Messenger.Subscribe(GameEventsPong.I_BALL_IN_TOP_GATE, BallInTopGate).AddTo(this);
            Messenger.Subscribe(GameEventsPong.I_BALL_IN_DOWN_GATE, BallInDownGate).AddTo(this);
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