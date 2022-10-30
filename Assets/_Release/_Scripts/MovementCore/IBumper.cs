using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// IObserva
    /// </summary>
    public interface IBumper: IObservable<BumpInfo>
    {
        Bounds GetBounds();
        void Bump(BumpInfo bumpInfo);
    }

    public struct BumpInfo
    {
        public BumperBase contactedBumper;
        public Vector3 bumpPoint;

        public BumpInfo(BumperBase contactedBumper, Vector3 bumpPoint)
        {
            this.contactedBumper = contactedBumper;
            this.bumpPoint = bumpPoint;
        }
    }

}