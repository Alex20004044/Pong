using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
public class UpgradeTest : MonoBehaviour
{
    [SerializeField]
    float currentTractorSpeedTest = 4f;
    [SerializeField]
    Upgrade upgrade;
    [Sirenix.OdinInspector.Button]
    void SetCurrentValue()
    {
        upgrade.SetCurrentValue(currentTractorSpeedTest);
    }

    [Sirenix.OdinInspector.Button]
    public void ApplyUpgrade()
    {
        //tractros.speed = upgrade.GetUpgradedValue();
        Debug.Log("Upgrade value is: " + upgrade.GetUpgradeCalculatedValue());
    }
}
