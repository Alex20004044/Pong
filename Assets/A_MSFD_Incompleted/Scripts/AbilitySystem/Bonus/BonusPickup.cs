using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{

    public class BonusPickup : MonoBehaviour
    {
        [SerializeField]
        List<AbilityBase> abilities;
        [SerializeField]
        InterfaceField<IZoneObserver> zoneObserver;
        [SerializeField]
        bool isDestroyOnTargetEnter = true;
        private void Awake()
        {
            zoneObserver.i.onTriggerEnterTransform += OnTargetEnter;
        }
        private void OnDestroy()
        {
            zoneObserver.i.onTriggerEnterTransform -= OnTargetEnter;
        }
        void OnTargetEnter(Transform target)
        {
            AbilityController abilityController = target.GetComponent<AbilityController>();
            foreach (AbilityBase x in abilities)
            {
                abilityController.ActivateAbility(x.gameObject);
            }
            if (isDestroyOnTargetEnter)
                Destroy(gameObject, 0.01f);
        }

    }
}
