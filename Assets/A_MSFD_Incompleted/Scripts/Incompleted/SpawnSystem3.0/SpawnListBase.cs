using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.SpawnSystem
{
    [System.Serializable]
    public abstract class SpawnListBase : ScriptableObject
    {
        public List<UnitDataBase> units;
        public WorkModeSpawnList workMode = WorkModeSpawnList.parallel;
        SpawnerBase spawnerPrefab;

        public GameObject GetSpawnerPrefab()
        {
            return spawnerPrefab.gameObject;
        }
        public SpawnerBase GetSpawnerType()
        {
            return spawnerPrefab;
        }
        public void SetSpawnerPrefab(SpawnerBase _spawnerPrefab)
        {
            spawnerPrefab = _spawnerPrefab;
        }
#if UNITY_EDITOR
        public float spawnListWeightCoefficient = 1f;
        
        public abstract void AddUnitDataField();
        public abstract bool RemoveUnitDataField();

        public abstract void AddWave();

        public abstract bool RemoveWave();
        public abstract int GetWavesCount();
        public abstract void DrawUnitField(int ind, float unitFieldWidth);
        /// <summary>
        /// This optional method is needed for additional parametres. It can do nothing. Use it with DrawUnderWaveColumn
        /// </summary>
        /// <param name="unitFieldWidth"></param>
        public abstract void DrawUnderUnitField(float unitFieldWidth);
        public abstract void DrawWaveColumn(int ind, float width);
        /// <summary>
        /// This optional method is needed for additional parametres. It can do nothing. Use it with DrawUnderUnitField
        /// </summary>
        /// <param name="width"></param>
        public abstract void DrawUnderWaveColumn(int ind, float width);
        public abstract float GetTotalWeight();
        public abstract float GetTotalTime();
        public float GetTotalPower()
        {
            return GetTotalWeight() / GetTotalTime();
        }
        public abstract float GetWeightInWave(int index);
        public abstract float GetTimeInWave(int index);
        public virtual float GetPowerInWave(int index)
        {
            return GetWeightInWave(index) / GetTimeInWave(index);
        }
#endif
    }
    public enum WorkModeSpawnList { parallel, consistent, individual};
}
