using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    [System.Serializable]
    public class Wave : System.ICloneable
    {
        public List<int> unitsNum = new List<int>();
        public int GroupsInWave
        {
            get
            {
                //For bugFix!!
                if (_groupsInWave < 1)
                {
                    _groupsInWave = 1;
                }
                return _groupsInWave;
            }
            set
            {
                if(value < 1)
                {
                    _groupsInWave = 1;
                }
                else
                {
                    _groupsInWave = value;
                }
            }
        }
        [SerializeField]
        int _groupsInWave = 1;

        public float groupsDelay = 0;
        public float unitsDelay = 0;
        public float wavesConditionTime = 3f;
        public float wavesDelay = 3;
        public SpawnList.NextWaveCondition nextWaveCondition = SpawnList.NextWaveCondition.allSpawnersReady;
//#if UNITY_EDITOR
//ICloneable не ревлизуется, если эта директива включена
        public bool isDisplayRightSpace;

        public Wave (int unitsCount)
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

        public object Clone()
        {
            Wave clone = (Wave) this.MemberwiseClone();
            clone.unitsNum = new List<int>(unitsNum.ToArray());
            return clone;
        }

        public void RemoveUnitCell()
        {
            unitsNum.RemoveAt(unitsNum.Count - 1);
        }
//#endif
    }
}