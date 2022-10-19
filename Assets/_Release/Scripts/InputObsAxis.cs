using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

namespace Pong
{
    public class InputObsAxis: MonoBehaviour, IObservable<float>
    {
        [SerializeField]
        string axisName = "Horizontal";

        [ReadOnly]
        [ShowInInspector]
        ReactiveProperty<float> input = new ReactiveProperty<float>();

        void Update()
        {
            input.Value = Input.GetAxis(axisName);
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return input.Subscribe(observer);
        }
    }
}