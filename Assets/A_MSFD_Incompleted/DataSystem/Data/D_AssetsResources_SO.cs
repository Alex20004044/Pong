using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "D_AssetsResources_", menuName = "DataSystem/D_AssetsResources")]
    public class D_AssetsResources_SO : DisplayDataBase
    { 
        [HorizontalGroup("Values")]
        [HideLabel]
        [SerializeField]
        D_AssetsResources d_value;
        public D_AssetsResources_SO()
        {
            dataName = DV.assetsName;
        }
        public override DataBase GetDataBase()
        {
            return d_value;
        }
        public D_AssetsResources GetData()
        {
            return d_value;
        }
        public static implicit operator D_AssetsResources(D_AssetsResources_SO display)
        {
            return display.d_value;
        }
    }
}