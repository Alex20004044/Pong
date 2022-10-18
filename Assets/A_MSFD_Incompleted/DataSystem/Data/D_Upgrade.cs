using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    public class D_Upgrade : DataBase
    {
        [TabGroup("Values")]
        //[ReadOnly]
        [SerializeField]
        int currentLevel = 0;
        [TabGroup("Values")]
        [SerializeField]
        float[] costs;

        public D_Upgrade(string dataPath, DataType dataType) : base(dataPath, dataType)
        {
        }

        public override void Save()
        {
            SaveCore.Save(GetPath(), currentLevel);
        }
        public override void Load()
        {
            currentLevel = SaveCore.Load<int>(GetPath(), 0);
            OnValueChanged();
        }
        /*
        [TabGroup("Values")]
        [Button("Add level")]*/
        public bool TryUpgrade()
        {
            if(IsUpgradePossible())
            { 
                currentLevel++;
                OnValueChanged();
                return true;
            }
            return false;
        }
        public bool IsUpgradePossible()
        {
            if(currentLevel < costs.Length)
            {
                return true;
            }
            return false;
        }
        public bool IsUpgradePossible(float unitsAmount)
        {
            if(IsUpgradePossible() && costs[currentLevel] <= unitsAmount)
            {
                return true;
            }
            return false;
        }

        public float[] GetCosts()
        {
            return costs;
        }

        public float GetCurrentLevelCost()
        {
            if(currentLevel >= costs.Length)
            {
                Debug.LogError("Current level > then available levels " + Name + "(" + currentLevel + ">" + costs.Length + ")");
                return float.PositiveInfinity;
            }
            return costs[currentLevel];
        }

        /*[TabGroup("Values")]
        [Button("GetCurrentLevel")]*/
        public int GetCurrentLevel()
        {
            return currentLevel;
        }
        public void Reset()
        {
            currentLevel = 0;
        }
        public float GetUpgradeProgress()
        {
            return (float)currentLevel / costs.Length;
        }
        public override string GetDataDescription()
        {
            return nameof(currentLevel) + ": " + currentLevel + "/" + costs.Length;
        }
    }
}