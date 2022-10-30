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
    }
}