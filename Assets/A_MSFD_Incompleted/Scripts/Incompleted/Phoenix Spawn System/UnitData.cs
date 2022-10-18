using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "UD_", menuName = "Phoenix Spawn System/Unit Data")]
    public class UnitData : UnitDataBase
    {
        [SerializeField]
        float weight = 1;


        public override float GetWeight()
        {
            return weight;
        }
    }
}