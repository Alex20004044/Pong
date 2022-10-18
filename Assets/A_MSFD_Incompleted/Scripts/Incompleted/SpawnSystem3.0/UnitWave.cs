using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.SpawnSystem
{
    [System.Serializable]
    public class UnitWave
    {
        public List<int> unitsNum = new List<int>();
        public int groupsInWave = 1;
        public float groupsDelay = 0;
        public float unitsDelay = 0;
        public SpawnList.NextWaveCondition nextWaveCondition = SpawnList.NextWaveCondition.allUnitsDead;
#if UNITY_EDITOR
        public UnitWave (int unitsCount)
        {
            for (int i = 0; i < unitsCount; i++)
            {
                unitsNum.Add(0);
            }
        }
        public void AddUnitCell()
        {
            unitsNum.Add(0);
        }
        public void RemoveUnitCell()
        {
            unitsNum.RemoveAt(unitsNum.Count - 1);
        }
#endif
    }
}