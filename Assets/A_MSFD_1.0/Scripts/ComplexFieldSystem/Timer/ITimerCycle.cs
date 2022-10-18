using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public interface ITimerCycle : ITimer
    {
        void SetCycleMode(bool isEnabled);
        bool IsInCycleMode();
        //Start cycle N times

    }
}