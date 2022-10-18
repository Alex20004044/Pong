using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace MSFD
{
    public abstract class BonusBase : AbilityBase
    {
        [Header("If duration <= 0 then bonus won't be deactivated")]
        [SerializeField]
        float bonusDuration = 0;
        [SerializeField]
        UnityEvent onActivationStart;

        protected GameObject unit;
        public override void Activate(GameObject _unit)
        {
            onActivationStart.Invoke();
            unit = _unit;
            if (bonusDuration > 0)
            {
                Invoke("Deactivate", bonusDuration);
            }
            ActivateBonus();
        }

        public override void Deactivate()
        {
            DeactivateBonus();
            Destroy(gameObject);
        }
        protected abstract void ActivateBonus();
        protected abstract void DeactivateBonus();

        public void SetBonusDuration(float duration)
        {
            bonusDuration = duration;
            if(unit != null)
            {
                Debug.LogError("Duration is refreshed, but it will not be applyed to current bonus lifetime");
            }
        }
        public float GetBonusDuration()
        {
            return bonusDuration;
        }
        public void AddListenerOnActivationStart(System.Action action)
        {
            onActivationStart.AddListener(new UnityAction(action));
        }
        public void RemoveListenerOnActivationStart(System.Action action)
        {
            onActivationStart.RemoveListener(new UnityAction(action));
        }
    }
}