using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace MSFD
{
    [Obsolete("Use Clip instead")]
    [System.Serializable]
    public class ClipCore
    {
        public CascadeModifier<float> maxCapacity = new CascadeModifier<float>(1);
        public CascadeModifier<float> shootCost = new CascadeModifier<float>(0.2f);
        public CascadeModifier<float> reloadPerSec = new CascadeModifier<float>(0.1f);
        public CascadeModifier<float> delayBetweenShoot = new CascadeModifier<float>(0.2f);

        float Charge
        {
            get
            {
                return __charge;
            }
            set
            {
                if(value > __charge)
                {
                    onIncreaseEnergy.Invoke();
                }
                else if(value < __charge)
                {
                    onDecreaseEnergy.Invoke();
                }

                

                if (value < 0)
                {
                    __charge = 0;
                    //Attention! Documentaion is needed
                    if (isCanShootWhenClipIsEmpty)
                    {
                        onEmpty.Invoke();
                    }
                }
                else if (value > maxCapacity)
                {
                    __charge = maxCapacity;
                    onFull.Invoke();
                }
                else
                {
                    __charge = value;
                    if(!isCanShootWhenClipIsEmpty && value < shootCost)
                    {
                        onEmpty.Invoke();
                    }
                }
            }
        }
        [LabelText("Charge")]
        [Obsolete("Use Charge instead")]
        [SerializeField]
        float __charge = 1f;

        [SerializeField]
        bool isCanShootWhenClipIsEmpty = false;
        [SerializeField]
        bool isReloadWhenCantShoot = true;

#if UNITY_EDITOR
        [Obsolete("Editor only")]
        [FoldoutGroup("Stats")]
        [OnStateUpdate("@chargeProgressBar = Charge")]
        [ProgressBar(0, "maxCapacity")]
        [ReadOnly]
        [ShowInInspector]
        float chargeProgressBar = 0;
#endif
        float RemainingTimeBeforeShoot
        {
            get
            {
                return __remainingTimeBeforeShoot;
            }
            set
            {
                if (value <= 0)
                {
                    __remainingTimeBeforeShoot = 0;
                }
                else
                {
                    __remainingTimeBeforeShoot = value;
                }
            }
        }
        [FoldoutGroup("Stats")]
        [Obsolete("Use RemainingTimeBeforeShoot instead")]
        [ReadOnly]
        [SerializeField]
        float __remainingTimeBeforeShoot = 0;


        #region Editor
#if UNITY_EDITOR
        [Obsolete("EditorOnly")]
        [FoldoutGroup("Stats")]
        [OnStateUpdate("@delayBetweenShootsWhenEmpty = GetDelayBetweenShootsWhenEmpty()")]
        [ReadOnly]
        [ShowInInspector]
        //[SerializeField]
        float delayBetweenShootsWhenEmpty;
        [Obsolete("EditorOnly")]
        [FoldoutGroup("Stats")]
        [OnStateUpdate("@dischargeTime = GetDischargeTime()")]
        [ReadOnly]
        [ShowInInspector]
        //[SerializeField]
        float dischargeTime;
        [Obsolete("EditorOnly")]
        [FoldoutGroup("Stats")]
        [OnStateUpdate("@dischargeShootsCount = GetDischargeShootsCount()")]
        [ReadOnly]
        [ShowInInspector]
        //[SerializeField]
        float dischargeShootsCount;
#endif
        #endregion

        [FoldoutGroup("Events")]
        public UnityEvent onEmpty = new UnityEvent();
        [FoldoutGroup("Events")]
        public UnityEvent onFull = new UnityEvent();
        [FoldoutGroup("Events")]
        public UnityEvent onShoot = new UnityEvent();
        [FoldoutGroup("Events")]
        public UnityEvent onIncreaseEnergy = new UnityEvent();
        [FoldoutGroup("Events")]
        public UnityEvent onDecreaseEnergy = new UnityEvent();
        /// <summary>
        /// Called when charge >= shootCost && RemainingTime == 0 one time
        /// </summary>
        [FoldoutGroup("Events")]
        public UnityEvent onCanShoot = new UnityEvent();


        float ClipUpdateDelay
        {
            get
            {
                return __clipUpdateDelay;
            }
            set
            {
                __clipUpdateDelay = Mathf.Clamp(value, 0.04f, float.PositiveInfinity);
            }
        }
        [FoldoutGroup("Debug")]
        [ShowIf("@clipUpdateRateMode == ClipUpdateRateMode.custom")]
        [Obsolete("Use ClipUpdateDelayInstead")]
        [Range(0.04f, float.PositiveInfinity)]
        [SerializeField]
        float __clipUpdateDelay = 0.1f;

        [FoldoutGroup("Debug")]
        [SerializeField]
        ClipUpdateRateMode clipUpdateRateMode = ClipUpdateRateMode.standart;

        MonoBehaviour source;
        /// <summary>
        /// It needs for smoothing fluctuations
        /// </summary>
        DateTime previousRechargeTime;


        bool isOnCanShootWasCalled = false;

        public void ActivateRechargeRoutine(MonoBehaviour _source)
        {
            ActivateRechargeRoutine(_source, clipUpdateRateMode);
        }
        public void ActivateRechargeRoutine(MonoBehaviour _source, ClipUpdateRateMode _clipUpdateRateMode = ClipUpdateRateMode.standart)
        {
            source = _source;
            clipUpdateRateMode = _clipUpdateRateMode;
            if(clipUpdateRateMode == ClipUpdateRateMode.standart)
            {
                ClipUpdateDelay = GameValues.clipUpdateRate;
            }
            RemainingTimeBeforeShoot = 0;
            source.StartCoroutine(ClipRoutine());
        }
        public void DeactivateRechargeRoutine()
        {
            source.StopCoroutine(ClipRoutine());
        }
        IEnumerator ClipRoutine()
        {
            previousRechargeTime = System.DateTime.Now;
            //Reload
            while (true)
            {
                float deltaTime = (float)(DateTime.Now - previousRechargeTime).TotalSeconds;
                
                if (isReloadWhenCantShoot || RemainingTimeBeforeShoot == 0)
                {
                    IncreaseCharge(reloadPerSec * deltaTime, deltaTime);
                }
                else
                {
                    RemainingTimeBeforeShoot -= deltaTime;
                }
                previousRechargeTime = System.DateTime.Now;
                yield return new WaitForSeconds(ClipUpdateDelay);
            }
        }

        /// <summary>
        /// Charge += amount;
        /// Actions: RemainingTimeBeforeShoot -= _decreseRemainingTimeBeforeShoot;
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="_decreseRemainingTimeBeforeShoot"></param>
        [FoldoutGroup("Debug")]
        [Button]
        public void IncreaseCharge(float amount, float _decreseRemainingTimeBeforeShoot)
        {
            Charge += amount;
            RemainingTimeBeforeShoot -= _decreseRemainingTimeBeforeShoot;
            if(!isOnCanShootWasCalled && IsCanShoot())
            {
                InvokeOnCanShoot();
            }
        }
        /// <summary>
        /// Charge -= amount;
        /// Actions: RemainingTimeBeforeShoot += _delayAfterShoot;
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="_delayAfterShoot"></param>
        [FoldoutGroup("Debug")]
        [Button]
        public void DecreaseCharge(float amount, float _delayAfterShoot)
        {
            Charge -= amount;
            RemainingTimeBeforeShoot += _delayAfterShoot;
            if(!IsCanShoot())
            {
                isOnCanShootWasCalled = false;
            }
        }
        [FoldoutGroup("Debug")]
        [Button]
        public void SetCharge(float amount)
        {
            Charge = amount;
        }
        [FoldoutGroup("Debug")]
        [Button]
        public void SetRemainingTime(float waitTime)
        {
            RemainingTimeBeforeShoot = waitTime;
        }
        [FoldoutGroup("Debug")]
        [Button]
        public bool TryShoot()
        {
            return TrySpecialShoot(shootCost, delayBetweenShoot, isCanShootWhenClipIsEmpty);
        }
        public bool TrySpecialShoot(float _shootCost, float _delayAfterShoot, bool _isCanShootWhenClipIsEmpty = false)
        {
            if (IsCanSpecialShoot(_shootCost, _isCanShootWhenClipIsEmpty))
            {
                onShoot.Invoke();
                DecreaseCharge(_shootCost, _delayAfterShoot);
                return true;
            }
            else
            {
                return false;
            }
        }
        public float GetCharge()
        {
            return Charge;
        }

        [FoldoutGroup("Debug")]
        [Button]
        public bool IsCanShoot()
        {
            return IsCanSpecialShoot(shootCost, isCanShootWhenClipIsEmpty);
        }
        public bool IsCanSpecialShoot(float _shootCost, bool _isCanShootWhenClipIsEmpty)
        {
            if (RemainingTimeBeforeShoot == 0 && (_isCanShootWhenClipIsEmpty || GetCharge() >= _shootCost))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetIsCanShootWhenClipIsEmtpy(bool b)
        {
            isCanShootWhenClipIsEmpty = b;
        }
        public bool GetIsCanShootWhenClipIsEmpty()
        {
            return isCanShootWhenClipIsEmpty;
        }
        public void SetIsReloadWhenCantShoot(bool b)
        {
            isReloadWhenCantShoot = b;
        }
        public bool GetIsReloadWhenCantShoot()
        {
            return isReloadWhenCantShoot;
        }
        public enum ClipUpdateRateMode { standart, custom };//, syncWithReloadPerSec};
        public ClipUpdateRateMode GetClipUpdateRateMode()
        {
            return clipUpdateRateMode;
        }

        void InvokeOnCanShoot()
        {
            isOnCanShootWasCalled = true;
            onCanShoot.Invoke();
        }
        #region Stats
        public float GetDelayBetweenShootsWhenEmpty()
        {
            return Mathf.Max(shootCost / reloadPerSec, delayBetweenShoot);
        }
        public float GetDischargeTime()
        {
            float dischargeTime;
            if (!isCanShootWhenClipIsEmpty && !isReloadWhenCantShoot)
            {
                //When capasity < shootCost
                dischargeTime = (((maxCapacity - (maxCapacity % shootCost))
                    / (shootCost)) - 1) * delayBetweenShoot;
            }
            else if (!isCanShootWhenClipIsEmpty && isReloadWhenCantShoot)
            {
                float deltaCapacity = (shootCost / delayBetweenShoot - reloadPerSec);
                if (maxCapacity > deltaCapacity)
                {
                    dischargeTime = (maxCapacity - (maxCapacity % deltaCapacity)) / deltaCapacity;
                }
                else
                {
                    dischargeTime = maxCapacity  / deltaCapacity;
                }
            }
            else if (isCanShootWhenClipIsEmpty && !isReloadWhenCantShoot)
            {
                dischargeTime = (maxCapacity / shootCost - 1) * delayBetweenShoot;
            }
            else//isCanShootWhenClipIsEmpty && isReloadWhenCantShoot
            {
                float deltaCapacity = (shootCost / delayBetweenShoot - reloadPerSec);
                dischargeTime = (maxCapacity) / deltaCapacity;
            }
            return dischargeTime;
        }
        /// <summary>
        /// Time to reload clip to make one shoot from zero charge
        /// </summary>
        /// <returns></returns>
        public float GetOneShootChargeTime()
        {
            return shootCost / reloadPerSec;
        }
        public float GetDischargeShootsCount()
        {

            if (!isCanShootWhenClipIsEmpty && !isReloadWhenCantShoot)
            {
                return (maxCapacity - (maxCapacity % shootCost)) / shootCost;
            }
            else if (!isCanShootWhenClipIsEmpty && isReloadWhenCantShoot)
            {
                return GetDischargeTime() / delayBetweenShoot;
            }
            else if (isCanShootWhenClipIsEmpty && !isReloadWhenCantShoot)
            {
                return Mathf.Ceil( maxCapacity / shootCost);
            }
            else//isCanShootWhenClipIsEmpty && isReloadWhenCantShoot
            {
                return Mathf.Ceil(GetDischargeTime() / delayBetweenShoot);
            }
        }
        #endregion Stats
    }
}