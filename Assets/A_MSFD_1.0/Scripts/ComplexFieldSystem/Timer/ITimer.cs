using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public interface ITimer
    {
        IObservable<Unit> GetObsOnTimeOver();
        void Start();
        void Stop();
        void Reset();
        void SetTime(float seconds);

        IObservable<bool> GetObsIsRechargeStarted();
        bool IsRechargeStarted();
        void SetTimeMode(TimeMode timeMode = TimeMode.scaledTime);
        //TimeMode GetTimeMode();
        enum TimeMode { scaledTime, realTime };
    }
}