using MSFD.AS;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Pong
{
    public abstract class BumperBase : MonoBehaviour, IBumper
    {
        Subject<BumpInfo> bumpSubject = new Subject<BumpInfo>();
        public abstract Bounds GetBounds();
        private void OnEnable()
        {
            SolidBody2DManager.Instance.RegisterSolidBody(this);
        }

        private void OnDisable()
        {
            SolidBody2DManager.Instance.UnregisterSolidBody(this);
        }




#if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position.SetZAxis(SolidBodyConstants.RENDER_SOLIDBODY2D_Z_POSITION), GetBounds().size);
        }

        public void Bump(BumpInfo bumpInfo)
        {
            bumpSubject.OnNext(bumpInfo);
        }

        public IDisposable Subscribe(IObserver<BumpInfo> observer)
        {
            return bumpSubject.Subscribe(observer);
        }
#endif
    }
}