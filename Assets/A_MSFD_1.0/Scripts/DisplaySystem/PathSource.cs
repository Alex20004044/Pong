using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class PathSource : MonoBehaviour, IObservable<string>, IObserver<string>
    {
        [SerializeField]
        string path;

        [SerializeField]
        PathMode pathMode = PathMode.absolute;
        [ShowIf("@pathMode == PathMode.relative")]
        [SerializeField]
        InterfaceField<IObservable<string>> prefixPathSource;

        ReactiveProperty<string> totalPath = new ReactiveProperty<string>();
        IDisposable disposable;
        void Awake()
        {
            if (pathMode == PathMode.relative)
                disposable = prefixPathSource.i.Subscribe(this).AddTo(gameObject);
            else
                totalPath.Value = path;
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            return totalPath.Subscribe(observer);
        }

        public void OnCompleted()
        {
            if(disposable != null)
                disposable.Dispose();
        }

        public void OnError(Exception error)
        {
            Debug.LogError(error);
            if (disposable != null)
                disposable.Dispose();
        }

        public void OnNext(string value)
        {
            totalPath.Value = value + "/" + path;
        }

        public enum PathMode { absolute, relative};
       
    }
}