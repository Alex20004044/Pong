using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;
using MSFD.Service;
using Sirenix.Utilities;
using UnityEngine.Events;
namespace MSFD
{
    public abstract class FieldObsDCBase<T> : MonoBehaviour, IObservable<T>, IObserver<T>
    {
        [SerializeField]
        InterfaceField<IObservable<string>> pathSource;

        [FoldoutGroup("Event", order: 10)]
        [SerializeField]
        UnityEvent onNext;

        Subject<T> subject = new Subject<T>();

        protected string path;
        protected IData data;

        IDisposable pathSetterDisposable;
        IDisposable dataDisposable;
        void Awake()
        {
            pathSetterDisposable = pathSource.i.Subscribe(OnPathRecieved);
        }

        void OnPathRecieved(string path)
        {
            this.path = path;
            if (dataDisposable != null)
                dataDisposable.Dispose();
            //Attention!
            if (!path.IsNullOrWhitespace())
            {            //Attention!
                if (DC.TryGetData(path, out data))
                {
                    dataDisposable = data.Subscribe(OnDataNext, OnError, OnCompleted);
                    OnDataNext(data);
                }
            }
        }

        protected abstract void OnDataNext(IData data);

#if UNITY_EDITOR
        [Obsolete("Editor Only!")]
        [Button]
        void AddPathSourceToGameObject()
        {
            PathSource pathSource = GetComponent<PathSource>();
            if (pathSource == null)
                this.pathSource.Set(gameObject.AddComponent<PathSource>());
            else
                this.pathSource.Set(pathSource);
        }
#endif


        [FoldoutGroup("Debug", order: 20)]
        [Button]
        public virtual void OnNext(T value)
        {
            onNext.Invoke();
            subject.OnNext(value);
        }
        [FoldoutGroup("Debug")]
        [Button]
        public virtual void OnCompleted()
        {
            subject.OnCompleted();
            dataDisposable.Dispose();
            pathSetterDisposable.Dispose();
        }
        [FoldoutGroup("Debug")]
        [Button]
        public virtual void OnError(Exception error)
        {
            subject.OnError(error);
        }
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return subject.Subscribe(observer);
        }
    }
}