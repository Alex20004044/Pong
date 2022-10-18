using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD.CH
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "D_Currency_", menuName = "DataSystem/D_Currency")]
    public class D_Currency_SO : DisplayDataBase
    { 
        [HorizontalGroup("Values")]
        [HideLabel]
        [SerializeField]
        D_Currency d_value;

        public D_Currency_SO()
        {
            dataName = DV.currencyName;
        }

        public override DataBase GetDataBase()
        {
            return d_value;
        }
        public D_Currency GetData()
        {
            return d_value;
        }
        public static implicit operator D_Currency(D_Currency_SO display)
        {
            return display.d_value;
        }
    }
}