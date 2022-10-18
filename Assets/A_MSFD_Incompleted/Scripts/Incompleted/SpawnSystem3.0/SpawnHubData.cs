using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.SpawnSystem
{
    [CreateAssetMenu(fileName = "_SpawnHub", menuName = "SpawnSystem/SpawnHub")]
    [System.Serializable]
    public class SpawnHubData : ScriptableObject
    {     


        public List<SpawnListBase> spawnLists;
        public List<SHDNextWaveCondition> nextWaveConditions = new List<SHDNextWaveCondition>();
        public List<float> delayBeforeNextWave = new List<float>();

        public int WavesCount
        {
            get
            {
                return __wavesCount;
            }
            set
            {
                int delta = value - __wavesCount;
                if (delta > 0)
                {
                    for (int i = 0; i < delta; i++)
                    {
                        delayBeforeNextWave.Add(0);
                        nextWaveConditions.Add(SHDNextWaveCondition.parallelsOrConsistent);
                    }
                }
                else if (delta < 0)
                {
                    for (int i = 0; i < -delta; i++)
                    {
                        delayBeforeNextWave.RemoveAt(delayBeforeNextWave.Count - 1);
                        nextWaveConditions.RemoveAt(nextWaveConditions.Count - 1);
                    }
                }
                __wavesCount = value;
            }
        }
        [SerializeField]
        int __wavesCount;
        public SHDNextWaveCondition GetNextWaveCondition(int waveInd)
        {
            return nextWaveConditions[waveInd];
        }
        public float GetDelayBeforeNextWave(int waveInd)
        {
            return delayBeforeNextWave[waveInd];
        }

#if UNITY_EDITOR

        public float GetTotalWeight()
        {
            float sum = 0;
            for (int i = 0; i < WavesCount; i++)
            {
                sum += GetWeightInWave(WavesCount);
            }
            return sum;
        }
        public float GetTotalTime()
        {
            float sum = 0;
            for (int i = 0; i < WavesCount; i++)
            {
                sum += GetTimeInWave(WavesCount);
            }
            //Need To improve
            return sum;
        }
        public float GetTotalPower()
        {
            return GetTotalWeight() / GetTotalTime();
        }


        public float GetWeightInWave(int index)
        {
            float sum = 0;
            for(int i =0; i < spawnLists.Count; i++)
            {
                sum += spawnLists[i].GetWeightInWave(index) * spawnLists[i].spawnListWeightCoefficient;
            }
            return sum;
        }
        public float GetTimeInWave(int index)
        {
            return 10 + Random.Range(0,index * 10);
            //Need To improve
        }
        public float GetPowerInWave(int index)
        {
            return GetWeightInWave(index) / GetTimeInWave(index);
        }

#endif
        public enum SHDNextWaveCondition { parallelsOrConsistent, allSpawners, externalActivation };
    }
}