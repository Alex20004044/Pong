using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class DeltaRangeToFloat : FieldObserverToBase<IDeltaRange<float>>, IObservable<float>
    {
        [DelayedProperty]
        [OnValueChanged("@Refresh()")]
        [SerializeField]
        Vector2 outputRange = new Vector2(0, 1);
        Subject<float> subject = new Subject<float>();

        IDeltaRange<float> deltaRangeFloat;

        public override void OnNext(IDeltaRange<float> value)
        {
            deltaRangeFloat = value;
            Refresh();
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {           
            observer.OnNext(MapValue());
            return ((IObservable<float>)subject).Subscribe(observer);
        }
        //[Button]
        void Refresh()
        {
            subject.OnNext(MapValue());
        }

        float MapValue()
        {
            if (deltaRangeFloat == null)
                return outputRange.x;
            return AS.Calculation.Map(deltaRangeFloat.Value, deltaRangeFloat.MinBorder, deltaRangeFloat.MaxBorder, 
                outputRange.x, outputRange.y);
        }
    }
}