using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public interface IBallUpgradable
    {
        IModifiable<float> GetSpeedModifiable();
    }
}