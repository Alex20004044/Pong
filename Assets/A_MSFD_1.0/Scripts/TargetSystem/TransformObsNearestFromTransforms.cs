using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UniRx;

namespace MSFD
{
    public class TransformObsNearestFromTransforms : MonoBehaviour, IObservable<Transform>, IObserver<Transform[]>
    {
        [SerializeField]
        InterfaceField<IObservable<Transform[]>> transformsSource;
        [SerializeField]
        float updateDelay = 1f;
        [ShowInInspector]
        [ReadOnly]
        ReactiveProperty<Transform> currentTarget = new ReactiveProperty<Transform>();

        Transform[] targets;
        private void Start()
        {
            transformsSource.i.Subscribe(this).AddTo(this);
            InvokeRepeating(nameof(UpdateTarget), 0, updateDelay);
        }
        
        void UpdateTarget()
        {
            currentTarget.Value = AS.Coordinates.FindNearestTarget(transform.position, targets);
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
            targets = value;
            UpdateTarget();
        }

        public IDisposable Subscribe(IObserver<Transform> observer)
        {
            return currentTarget.Subscribe(observer);
        }


    }
}