using MSFD;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Pong
{
    public class SizeChangableTransform : NetworkBehaviour, ISizeChangable
    {
        ModField<float> sizeMod = new ModField<float>(1);

        Vector3 initScale;

        void Awake()
        {
            initScale = transform.localScale;
            sizeMod.GetObsOnModsUpdated().Subscribe((_) => OnSizeModUpdated()).AddTo(this);
        }

        void OnSizeModUpdated()
        {
           SetSizeClientRPC(new Vector3(initScale.x * sizeMod, initScale.y, initScale.z));
        }
        [ClientRpc]
        void SetSizeClientRPC(Vector3 size)
        {
            transform.localScale = size;
        }

        public IModifiable<float> GetSizeModifiable()
        {
            return sizeMod;
        }
    }
}