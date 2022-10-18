using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    
    [System.Serializable]
    public class D_String : DataBase
    {
        [TabGroup("Values")]
        [SerializeField]
        string value;

        public D_String(string dataPath, DataType dataType, string _value) : base(dataPath, dataType)
        {
            value = _value;
        }

        public string GetValue()
        {
            return value;
        }
        public void SetValue(string _value)
        {
            value = _value;
            OnValueChanged();
        }
        public override void Load()
        {
            SetValue(SaveCore.Load<string>(GetPath(), value));
        }

        public override void Save()
        {
            SaveCore.Save(GetPath(), value);
        }
        public override string GetDataDescription()
        {
            return value;
        }
    }
}
