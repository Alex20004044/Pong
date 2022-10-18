using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    public class Upgrade
    {
        [FoldoutGroup("Service")]
        [SerializeField]
        RecieveUpgradeDataMode recieveUpgradeDataMode = RecieveUpgradeDataMode.dataCore;

        [FoldoutGroup("Service")]
        [ShowIf("@recieveUpgradeDataMode == RecieveUpgradeDataMode.manual")]
        [SerializeField]
        D_Upgrade_SO d_Upgrade_SO;
        [FoldoutGroup("Service")]
        [ShowIf("@recieveUpgradeDataMode == RecieveUpgradeDataMode.dataCore")]
        [SerializeField]
        string pathToUpgradeData;

        [SerializeField]
        CalculateUpgradeMode calculateUpgradeMode = CalculateUpgradeMode.lerpBetweenMinMax;
        [HorizontalGroup("MinMax")]
        [ShowIf("@calculateUpgradeMode == CalculateUpgradeMode.lerpBetweenMinMax")]
        [SerializeField]
        float minValue = 0;
        [HorizontalGroup("MinMax")]
        [ShowIf("@calculateUpgradeMode == CalculateUpgradeMode.lerpBetweenMinMax || calculateUpgradeMode == CalculateUpgradeMode.lerpBetweenCurrentMax")]
        [SerializeField]
        float maxValue = 1;

        [ShowIf("@calculateUpgradeMode == CalculateUpgradeMode.curveEvaluation")]
        [SerializeField]
        AnimationCurve valueCurve;

        float? currentValue;

        public Upgrade(string _pathToUpgradeData, float _minValue, float _maxValue)
        {
            recieveUpgradeDataMode = RecieveUpgradeDataMode.dataCore;
            calculateUpgradeMode = CalculateUpgradeMode.lerpBetweenMinMax;
            pathToUpgradeData = _pathToUpgradeData;
            minValue = _minValue;
            maxValue = _maxValue;
        }
        /// <summary>
        /// Method set calculateUpgradeMode = CalculateUpgradeMode.lerpBetweenCurrentMax
        /// </summary>
        /// <param name="_currentValue"></param>
        public void SetCurrentValue(float _currentValue)
        {
            calculateUpgradeMode = CalculateUpgradeMode.lerpBetweenCurrentMax;
            currentValue = _currentValue;
        }
        [Button]
        public float GetUpgradeCalculatedValue()
        {
            return CalculateUpgradeValue();
        }    
        public float GetRowUpgradeValue()
        {
            return GetUpgrade().GetUpgradeProgress();
        }
        
        float CalculateUpgradeValue()
        {
            D_Upgrade upgrade = GetUpgrade();
            if (upgrade == null)
            {
                Debug.LogError("Upgrade will not be applied");
                return GetMinValue();
            }
            if (calculateUpgradeMode == CalculateUpgradeMode.curveEvaluation)
            {
                return valueCurve.Evaluate(upgrade.GetUpgradeProgress());
            }
            else
            {
                float _minValue = GetMinValue();
                return Mathf.Lerp(_minValue, maxValue, upgrade.GetUpgradeProgress());
            }
        }
        public D_Upgrade GetUpgrade()
        {
            if (recieveUpgradeDataMode == RecieveUpgradeDataMode.manual)
            {
                return d_Upgrade_SO.GetData();
            }
            else
            {
                return DC.GetData<D_Upgrade>(pathToUpgradeData);
            }
        }
        float GetMinValue()
        {
            if (calculateUpgradeMode == CalculateUpgradeMode.lerpBetweenMinMax)
            {
                return minValue;
            }
            else
            {
                if(currentValue == null)
                {
                    Debug.LogError("Current value is not installed. You must call SetCurrentValue() in script to use leprBetweenCurrentMax mode");
                    currentValue = minValue;
                }
                return currentValue.Value;
            }
        }
        public void SetRecieveUpgradeDataMode(string _pathToUpgradeData)
        {
            recieveUpgradeDataMode = RecieveUpgradeDataMode.dataCore;
            pathToUpgradeData = _pathToUpgradeData;
        }
        public void SetRecieveUpgradeDataMode(D_Upgrade_SO _d_Upgrade_SO)
        {
            recieveUpgradeDataMode = RecieveUpgradeDataMode.manual;
            d_Upgrade_SO = _d_Upgrade_SO;
        }
        enum RecieveUpgradeDataMode { manual, dataCore };
        enum CalculateUpgradeMode { lerpBetweenMinMax, lerpBetweenCurrentMax, curveEvaluation };
    }
}