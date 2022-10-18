using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class TransformObsFirstFromTransforms : MonoBehaviour, IObservable<Transform>, IObserver<Transform[]>
    {
        [SerializeField]
        InterfaceField<IObservable<Transform[]>> transformsSource;

        [ShowInInspector]
        [ReadOnly]
        ReactiveProperty<Transform> currentTarget = new ReactiveProperty<Transform>();
        private void Start()
        {
            transformsSource.i.Subscribe(this).AddTo(this);
        }

        public void OnCompleted()
        {
            currentTarget.Dispose();
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(Transform[] value)
        {
            if (value == null)
                currentTarget.Value = null;
            else
                currentTarget.Value = value[0];
        }

        public IDisposable Subscribe(IObserver<Transform> observer)
        {
            return currentTarget.Subscribe(observer);
        }
    }
}