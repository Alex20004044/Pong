using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.SpawnSystem
{
    [CreateAssetMenu(fileName = "UnitDataChild", menuName = "Data/UnitDataChild")]
    [System.Serializable]
    public class UnitDataChild : UnitDataBase
    {
        public string child = "ITS A UNIT CHILD";
    }
}