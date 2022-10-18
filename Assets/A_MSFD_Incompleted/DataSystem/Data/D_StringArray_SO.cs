using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "D_StringArray_", menuName = "DataSystem/D_StringArray")]
    public class D_StringArray_SO : DisplayDataBase
    { 
        [HorizontalGroup("Values")]
        [HideLabel]
        [SerializeField]
        D_StringArray d_value;
        public override DataBase GetDataBase()
        {
            return d_value;
        }
        public D_StringArray GetData()
        {
            return d_value;
        }
        public static implicit operator D_StringArray(D_StringArray_SO display)
        {
            return display.d_value;
        }
    }
}