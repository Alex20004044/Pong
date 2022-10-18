using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace MSFD.Data
{
    public class DisplayD_Upgrade : DisplayBase
    {
        [SerializeField]
        TMP_Text upgradeNameText;
        [SerializeField]
        TMP_Text upgradeCostText;
        [SerializeField]
        Button upgradeButton;
        [Header("Scrpit choose one state whether upgrade is possible or not")]
        [SerializeField]
        GameObject upgradePossibleStateGO;
        [SerializeField]
        GameObject upgradeImpossibleStateGO;
        [SerializeField]
        Slider upgradeProgress;

        [Space]
        [SerializeField]
        string upgradeRequestMessage = DV.STRING_UPGRADE_REQUEST;

        D_Upgrade upgrade;
        public override void Initialize()
        {
            upgrade = data as D_Upgrade;
            upgradeButton.onClick.AddListener(UpgradeRequest);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            upgradeButton.onClick.RemoveListener(UpgradeRequest);
        }
        public override void Display()
        {
            upgradeNameText.text = upgrade.GetDataName();
            if(upgrade.IsUpgradePossible())
            {
                upgradeButton.gameObject.SetActive(true);
                upgradeCostText.text = upgrade.GetCurrentLevelCost().ToString();

                if(upgradePossibleStateGO != null)
                    upgradePossibleStateGO.SetActive(true);
                if (upgradeImpossibleStateGO != null)
                    upgradeImpossibleStateGO.SetActive(false);
            }
            else
            {
                upgradeButton.gameObject.SetActive(false);
                upgradeCostText.text = "Max";
                if (upgradePossibleStateGO != null)
                    upgradePossibleStateGO?.SetActive(false);
                if (upgradeImpossibleStateGO != null)
                    upgradeImpossibleStateGO?.SetActive(true);
            }
            upgradeProgress.value = upgrade.GetUpgradeProgress();
        }
        void UpgradeRequest()
        {
            Messenger<string>.Broadcast(upgradeRequestMessage, data.GetPath());
        }
    }
}