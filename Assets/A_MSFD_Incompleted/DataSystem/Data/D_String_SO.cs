using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "D_String_", menuName = "DataSystem/D_String")]
    public class D_String_SO : DisplayDataBase
    { 
        [HorizontalGroup("Values")]
        [HideLabel]
        [SerializeField]
        D_String d_value;

        public override DataBase GetDataBase()
        {
            return d_value;
        }
        public D_String GetData()
        {
            return d_value;
        }
        public static implicit operator D_String(D_String_SO display)
        {
            return display.d_value;
        }

    }
}