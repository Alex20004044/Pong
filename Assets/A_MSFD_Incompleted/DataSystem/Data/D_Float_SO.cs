using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "D_Float_", menuName = "DataSystem/D_Float")]
    public class D_Float_SO : DisplayDataBase
    {
        [HorizontalGroup("Values")]
        [HideLabel]
        [SerializeField]
        D_Float d_value;
        public override DataBase GetDataBase()
        {
            return d_value;
        }
        public D_Float GetData()
        {
            return d_value;
        }
        public static implicit operator D_Float(D_Float_SO display)
        {
            return display.d_value;
        }
    }
}
