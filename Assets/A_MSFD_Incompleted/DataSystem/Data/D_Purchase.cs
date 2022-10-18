using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    public class D_Purchase : DataBase
    {
        [TabGroup("Values")]
        [SerializeField]
        bool isBought = false;
        [TabGroup("Values")]
        [SerializeField]
        bool isUnlocked = true;
        [TabGroup("Values")]
        [SerializeField]
        float cost = 0;

        public D_Purchase(string dataPath, DataType dataType) : base(dataPath, dataType)
        {
        }

        public bool IsBought()
        {
            return isBought;
        }
        public bool IsUnlocked()
        {
            return isUnlocked;
        }
        public float GetCost()
        {
            return cost;
        }
        
        public void SetIsBought(bool _isBought)
        {
            isBought = _isBought;
            OnValueChanged();
        }
        public void SetIsUnlocked(bool _isUnlocked)
        {
            isUnlocked = _isUnlocked;
            OnValueChanged();
        }

        public bool IsCanBuy(float unitsAmount)
        {
            if (isBought)
            {
                Debug.LogError("Attempt to buy bought item " + Name);
            }

            if(!isBought && isUnlocked && cost <= unitsAmount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void Save()
        {
            SaveCore.Save(GetPath() + "_" + nameof(isBought), isBought);
            SaveCore.Save(GetPath() + "_" + nameof(isUnlocked), isUnlocked);
        }

        public override void Load()
        {
            isBought = SaveCore.Load<bool>(GetPath() + "_" + nameof(isBought), isBought);
            isUnlocked = SaveCore.Load<bool>(GetPath() + "_" + nameof(isUnlocked), isUnlocked);
        }
        public override string GetDataDescription()
        {
            return nameof(isBought) + ": "+ isBought + " / " + nameof(isUnlocked) + ": " + isUnlocked + " / " + nameof(cost) + ": " + cost;
        }
    }
}
