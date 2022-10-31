using MSFD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class BonusBallAcceleration : BonusBase
    {
        [SerializeField]
        float speedMultiplyer = 2;

        IBallUpgradable ballUpgradable;
        IDisposable disposable;
        protected override void ActivateBonus()
        {
            ballUpgradable = unit.GetComponent<IBallUpgradable>();
            disposable = ballUpgradable.GetSpeedModifiable().AddMod(SpeedMultiplyer);
        }

        protected override void DeactivateBonus()
        {
            disposable.Dispose();
        }
        float SpeedMultiplyer(float speed)
        {
            return speed * speedMultiplyer;
        }
    }
}