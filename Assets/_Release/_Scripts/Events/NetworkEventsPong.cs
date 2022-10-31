using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    [MessengerEventContainer]
    public static class NetworkEventsPong 
    {
        public const string R_START_HOST = "R_START_HOST";
        public const string R_START_CLIENT = "R_START_CLIENT";
        public const string R_CANCEL_CLIENT_CONNECTION = "R_CANCEL_CLIENT_CONNECTION";
        public const string R_DISCONNECT_FROM_ROOM = "R_DISCONNECT_FROM_ROOM";

        public const string R_MultiplayerMode_SET_MULTIPLAYER_MODE = "R_MultiplayerMode_SET_MULTIPLAYER_MODE";

        public enum MultiplayerMode { local, global};

    }
}