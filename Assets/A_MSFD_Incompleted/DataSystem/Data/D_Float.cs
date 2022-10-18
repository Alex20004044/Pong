using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    public class D_Float : DataBase
    {
        [TabGroup("Values")]
        [SerializeField]
        float value;

        [TabGroup("System")]
        [SerializeField]
        bool isBroadcastFloatValue = false;

        public D_Float(string dataPath, DataType dataType) : base(dataPath, dataType)
        {
        }
        public float GetValue()
        {
            return value;
        }

        public override void Load()
        {
            SetValue(SaveCore.Load<float>(GetPath(), value));
        }

        public override void Save()
        {
            SaveCore.Save(GetPath(), value);
        }

        public void SetValue(float  _value)
        {
            value = _value;
            OnValueChanged();
        }
        public void AddValue(float addAmount)
        {
            value += addAmount;
            OnValueChanged();
        }
        public void MultiplyValue(float multiplyer)
        {
            value *= multiplyer;
            OnValueChanged();
        }
        public override void OnValueChanged()
        {
            base.OnValueChanged();
            if (isBroadcastFloatValue)
            {
                Messenger<float>.Broadcast(MessengerValues.floatEventPrefix + GetBroadcastedMessage(), value, MessengerMode.DONT_REQUIRE_LISTENER);
            }
        }
        public override string GetDataDescription()
        {
            return value.ToString();
        }

    }
}
