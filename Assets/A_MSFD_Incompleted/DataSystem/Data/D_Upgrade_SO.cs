using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "D_Upgrade_", menuName = "DataSystem/D_Upgrade")]
    public class D_Upgrade_SO : DisplayDataBase
    {
        [HorizontalGroup("Values")]
        [HideLabel]
        [SerializeField]
        D_Upgrade d_value;

        public D_Upgrade_SO()
        {
            dataName = DV.upgradeName;
        }
        public override DataBase GetDataBase()
        {
            return d_value;
        }
        public D_Upgrade GetData()
        {
            return d_value;
        }
        public static implicit operator D_Upgrade(D_Upgrade_SO display)
        {
            return display.d_value;
        }
    }
}