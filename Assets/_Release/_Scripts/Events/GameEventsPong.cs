using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    [MessengerEventContainer]
    public static class GameEventsPong
    {
        public const string R_int_ADD_SCORE_TO_PLAYER = "R_int_ADD_SCORE_TO_PLAYER";
        public const string I_int_int_PLAYER_SCORE_UPDATED = "I_int_PLAYER_SCORE_UPDATED";

        public const string I_float_TIME_BEFORE_START_GAME = "I_float_TIME_BEFORE_START_GAME";

        public const string R_string_DISPLAY_MESSAGE = "R_string_DISPLAY_MESSAGE";

        public const string I_string_SCOREBOARD = "I_string_SCOREBOARD";

        public const string I_ROUND_STARTED = "I_ROUND_STARTED";
        public const string I_BALL_IN_TOP_GATE = "I_BALL_IN_TOP_GATE";
        public const string I_BALL_IN_DOWN_GATE = "I_BALL_IN_DOWN_GATE";



    }
}