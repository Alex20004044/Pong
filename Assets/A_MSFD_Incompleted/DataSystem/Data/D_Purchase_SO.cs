using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "D_Purchase_", menuName = "DataSystem/D_Purchase")]
    public class D_Purchase_SO : DisplayDataBase
    {
        [HorizontalGroup("Values")]
        [HideLabel]
        [SerializeField]
        D_Purchase d_value;

        public D_Purchase_SO()
        {
            dataName = DV.purchaseName;
        }

        public override DataBase GetDataBase()
        {
            return d_value;
        }
        public D_Purchase GetData()
        {
            return d_value;
        }
        public static implicit operator D_Purchase(D_Purchase_SO display)
        {
            return display.d_value;
        }
    }
}