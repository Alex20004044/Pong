using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class FloatConverterToFloatTransition : FieldConverterToBase<float, float>
    {
        [SerializeField]
        FloatObsTransitionCore transitionCore = new FloatObsTransitionCore();

        public override void OnNext(float value)
        {
            transitionCore.OnNext(value);
        }

        public override IDisposable Subscribe(IObserver<float> observer)
        {
            return transitionCore.Subscribe(observer);
        }
    }
}