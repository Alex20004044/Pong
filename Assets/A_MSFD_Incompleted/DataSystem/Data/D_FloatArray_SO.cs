using Sirenix.OdinInspector;
using UnityEngine;

namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "D_FloatArray_", menuName = "DataSystem/D_FloatArray")]
    public class D_FloatArray_SO : DisplayDataBase
    { 
        [HorizontalGroup("Values")]
        [HideLabel]
        [SerializeField]
        D_FloatArray d_value;
        public override DataBase GetDataBase()
        {
            return d_value;
        }
        public D_FloatArray GetData()
        {
            return d_value;
        }
        public static implicit operator D_FloatArray(D_FloatArray_SO display)
        {
            return display.d_value;
        }
    }
}