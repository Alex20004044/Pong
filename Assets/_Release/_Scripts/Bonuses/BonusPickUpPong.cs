using MSFD;
using System;
using System.Collections.Generic;
using UniRx;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    [RequireComponent(typeof(BumperBase))]
    public class BonusPickUpPong : NetworkBehaviour
    {
        [SerializeField]
        bool isDestroyOnTargetEnter = true;

        [SerializeField]
        List<AbilityBase> abilities;
        public override void OnNetworkSpawn()
        {
            if (!IsServer) return;
            GetComponent<BumperBase>().Subscribe(OnBump).AddTo(this);
        }

        void OnBump(BumpInfo bumpInfo)
        {
            if (bumpInfo.contactedBumper.gameObject.tag != GameValuesPong.ballTag) return;
            AbilityControllerPong abilityController = bumpInfo.contactedBumper.gameObject.GetComponent<AbilityControllerPong>();
            foreach (AbilityBase x in abilities)
            {
                abilityController.ActivateAbility(x.gameObject);
            }
            if (isDestroyOnTargetEnter)
                Invoke(nameof(Destruct), 0.01f);
        }
        void Destruct()
        {
            GetComponent<NetworkObject>().Despawn(true);
        }
    }
}