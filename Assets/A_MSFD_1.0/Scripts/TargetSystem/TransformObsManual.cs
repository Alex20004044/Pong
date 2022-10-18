using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class TransformObsManual : MonoBehaviour, IObservable<Transform>
    {
        [SerializeField]
        ReactiveProperty<Transform> target;
        public IDisposable Subscribe(IObserver<Transform> observer)
        {
            return target.Subscribe(observer);
        }

        [Button]
        void Refresh()
        {
            target.SetValueAndForceNotify(target.Value);
        }
    }
}