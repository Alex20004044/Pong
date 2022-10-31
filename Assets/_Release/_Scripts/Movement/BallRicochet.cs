using MSFD;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Pong
{
    public class BallRicochet : MonoBehaviour
    {
        [SerializeField]
        CorD.SparrowInterfaceField.InterfaceField<IObservable<BumpInfo>> bumper;
        [SerializeField]
        WorkMode workMode = WorkMode.reflectHorizontal;

        private void Awake()
        {
            bumper.i.Subscribe(OnBump).AddTo(this);
        }

        void OnBump(BumpInfo bumpInfo)
        {
            GameObject go= bumpInfo.contactedBumper.gameObject;
            if (go.tag != GameValuesPong.ballTag) return;

            if(workMode == WorkMode.reflectHorizontal)
                go.GetComponent<BallMovementSolidBody>().ReflectHorizontalDirection();
            else if(workMode == WorkMode.reflectVertical)
                go.GetComponent<BallMovementSolidBody>().ReflectVerticalDirection();
        }

        enum WorkMode { reflectHorizontal, reflectVertical};
    }
}