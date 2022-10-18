using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [RequireComponent(typeof(BonusBase))]
    public class BonusBaseUpgrader : MonoBehaviour
    {
        [SerializeField]
        RecieveUpgradeDataMode recieveUpgradeDataMode = RecieveUpgradeDataMode.dataCore;

        [ShowIf("@recieveUpgradeDataMode == RecieveUpgradeDataMode.manual")]
        [SerializeField]
        D_Upgrade_SO d_Upgrade_SO;
        [ShowIf("@recieveUpgradeDataMode == RecieveUpgradeDataMode.dataCore")]
        [SerializeField]
        string pathToUpgradeData;

        [SerializeField]
        CalculateUpgradeMode calculateUpgradeMode = CalculateUpgradeMode.lerpBetweenMinMax;
        [ShowIf("@calculateUpgradeMode == CalculateUpgradeMode.lerpBetweenMinMax")]
        [SerializeField]
        float minTimeValue = 1;
        [SerializeField]
        float maxTimeValue = 5;


        BonusBase bonusBase;
        private void Awake()
        {
            bonusBase = GetComponent<BonusBase>();
            bonusBase.AddListenerOnActivationStart(ApplyUpgrade);
        }
        private void OnDestroy()
        {
            bonusBase.RemoveListenerOnActivationStart(ApplyUpgrade);
        }
        void ApplyUpgrade()
        {
            bonusBase.SetBonusDuration(CalculateBonusDuration());
        }
        float CalculateBonusDuration()
        {
            D_Upgrade upgrade = GetUpgrade();
            if(upgrade == null)
            {
                Debug.LogError("Upgrade will not be applied");
                return bonusBase.GetBonusDuration();
            }
            float _minTimeValue = GetMinValue();
            return Mathf.Lerp(_minTimeValue, maxTimeValue, upgrade.GetUpgradeProgress());
        }
        D_Upgrade GetUpgrade()
        {
            if(recieveUpgradeDataMode == RecieveUpgradeDataMode.manual)
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
            if(calculateUpgradeMode == CalculateUpgradeMode.lerpBetweenMinMax)
            {
                return minTimeValue;
            }
            else
            {
                return bonusBase.GetBonusDuration();
            }
        }
        enum RecieveUpgradeDataMode { manual, dataCore};
        enum CalculateUpgradeMode { lerpBetweenMinMax, leprBetweenCurrentMax};
    }
}
