using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace MSFD
{

    [System.Serializable]
    public class D_Currency : DataBase
    {
        [TabGroup("Values")]
        [SerializeField]
        CurrencyType value;

        public D_Currency(string dataPath, DataType dataType) : base(dataPath, dataType)
        {
        }

        public CurrencyType GetCurrencyType()
        {
            return value;
        }

        public override string GetDataDescription()
        {
            return nameof(CurrencyType) + ": " + value;
        }

    }

}

