using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class RegisterGoalsDebug : MonoBehaviour
    {
        [SerializeField]
        GoalRegistrator goalRegistrator;
        public void RegisterGoalFirstPlayerWon()
        {
            goalRegistrator.RegisterGoal(PlayerEnum.playerOne);
        }
        public void RegisterGoalSecondPlayerWon()
        {
            goalRegistrator.RegisterGoal(PlayerEnum.playerTwo);
        }
    }
}