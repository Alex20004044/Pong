using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MSFD.PhoenixSpawnSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "_SpawnList", menuName = "Phoenix Spawn System/Spawn List")]
    public class SpawnList : ScriptableObject
    {
        public List<UnitDataBase> units = new List<UnitDataBase>();
        public List<Wave> waves = new List<Wave>();
        public float defaultWavesDelay = 3f;
        public UnitsPerGroupAllocation unitsPerGroupAllocation;

        public string groupsInWaveName = "Groups in Wave: ";
        public string groupsDelayName = "Groups Delay: ";
        public string unitsDelayName = "Units Delay: ";
        public string wavesDelayName = "Waves Delay:";

        public string nextWaveTimeName = "NextWaveTime: ";
        public string nextWaveTimeCloseName = "closed";
        public string nextWaveConditionName = "Next Wave Condition: ";

        float defaultSmallSpace = 20f;


        public List<UnitRenderData> unitRenderData = new List<UnitRenderData>();
        public UnitsWeightMode unitsWeightMode = UnitsWeightMode.normal;
        public bool isUseDefaultWaveDelay = true;
        public bool isDisplayUnitDescription = false;

        public float columnWidth = 40f;

        public float totalWeightMultiplyer = 1f;
        public float totalAdditionalWeight = 0;

        public float totalTimeMultiplyer = 1f;
        public float totalAdditionaTime = 0;


        static float minUnitsDelay = 0.02f;

        public float GetWaveDelay(int waveInd)
        {
            if(isUseDefaultWaveDelay)
            {
                return defaultWavesDelay;
            }
            else
            {
                return waves[waveInd].wavesDelay;
            }
        }


        public void AddUnitDataField()
        {
            units.Add(null);
            unitRenderData.Add(new UnitRenderData());
            for (int i = 0; i < waves.Count; i++)
            {
                waves[i].AddUnitCell();
            }
        }
        public bool RemoveUnitDataField()
        {
            if (units.Count > 0)
            {
                units.RemoveAt(units.Count - 1);
                unitRenderData.RemoveAt(unitRenderData.Count - 1);
                for (int i = 0; i < waves.Count; i++)
                {
                    waves[i].RemoveUnitCell();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddWave()
        {
            if (waves.Count > 1)
            {
                waves.Add((Wave)waves[waves.Count - 1].Clone());
            }
            else
            {
                waves.Add(new Wave(units.Count));
            }
        }
        public  bool RemoveWave()
        {
            if (waves.Count > 0)
            {
                waves.RemoveAt(waves.Count - 1);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetWavesCount()
        {
            return waves.Count;
        }
#if UNITY_EDITOR
        public void DrawUnitField(int ind, float unitFieldWidth)
        {
            
            units[ind] = (UnitDataBase)UnityEditor.EditorGUILayout.ObjectField("", units[ind], typeof(UnitDataBase),
            false, GUILayout.Width(unitFieldWidth));
        }
        public void DrawUnderUnitField(float unitFieldWidth)
        {
            EditorGUILayout.LabelField(groupsInWaveName, GUILayout.Width(unitFieldWidth));
            EditorGUILayout.LabelField(groupsDelayName, GUILayout.Width(unitFieldWidth));
            EditorGUILayout.LabelField(unitsDelayName, GUILayout.Width(unitFieldWidth));
            EditorGUILayout.Space(defaultSmallSpace);

            //if (!isUseDefaultWaveDelay)
            //{
                EditorGUILayout.LabelField(wavesDelayName, GUILayout.Width(unitFieldWidth));
           // }


            EditorGUILayout.LabelField(nextWaveTimeName, GUILayout.Width(unitFieldWidth));
            EditorGUILayout.LabelField(nextWaveConditionName, GUILayout.Width(unitFieldWidth));
        }
        public void DrawUnderWaveColumn(int ind, float width)
        {
            waves[ind].GroupsInWave = EditorGUILayout.IntField(waves[ind].GroupsInWave, GUILayout.Width(width));
            waves[ind].groupsDelay = EditorGUILayout.FloatField(waves[ind].groupsDelay, GUILayout.Width(width));
            waves[ind].unitsDelay = EditorGUILayout.FloatField(waves[ind].unitsDelay, GUILayout.Width(width));
            EditorGUILayout.Space(defaultSmallSpace);

            if (!isUseDefaultWaveDelay)
            {
                waves[ind].wavesDelay = EditorGUILayout.FloatField(waves[ind].wavesDelay, GUILayout.Width(width));
            }
            else
            {
                EditorGUILayout.LabelField(defaultWavesDelay.ToString(), GUILayout.Width(width));
            }


            if (waves[ind].nextWaveCondition == NextWaveCondition.timeWait || waves[ind].nextWaveCondition == NextWaveCondition.waitTimeOrAllSpawnersReady)
            {
                waves[ind].wavesConditionTime = EditorGUILayout.FloatField(waves[ind].wavesConditionTime, GUILayout.Width(width));
            }
            else
            {
                EditorGUILayout.LabelField(nextWaveTimeCloseName, GUILayout.Width(width));
            }

            waves[ind].nextWaveCondition = (NextWaveCondition)EditorGUILayout.EnumPopup(waves[ind].nextWaveCondition, GUILayout.Width(width));
        }
#endif
        public float GetTotalWeight()
        {
            float sum = 0;
            for (int i = 0; i < waves.Count; i++)
            {
                sum += GetWeightInWave(i);
            }
            sum = sum * totalWeightMultiplyer + totalAdditionalWeight;
            return sum;
        }
        public float GetTotalTime()
        {
            float sum = 0;
            for (int i = 0; i < waves.Count; i++)
            {
                sum += GetTimeInWave(i);
            }
            sum = sum * totalTimeMultiplyer + totalAdditionaTime;
            return sum;
        }
        public float GetTotalPower()
        {
            return GetTotalWeight() / GetTotalTime();
        }
        public float GetWeightInWave(int index)
        {
            float sum = 0;
            float unitWeight;
            for(int i =0; i < units.Count; i++)
            {
                if (units[i] != null)
                {
                    unitWeight = units[i].GetWeight();              
                    if(unitsWeightMode == UnitsWeightMode.multiplyByCoefficient)
                    {
                        unitWeight *= unitRenderData[i].unitWeightCoefficient;
                    }

                    sum += unitWeight * waves[index].unitsNum[i];
                }
            }

            return sum;
        }
        public float GetTimeInWave(int index)
        {
            Wave wave = waves[index];

            float sum = GetSpawnTimeInWave(index);

            if(isUseDefaultWaveDelay)
            {
                sum += defaultWavesDelay;
            }
            else
            {
                sum += wave.wavesDelay;
            }
            if (wave.nextWaveCondition == NextWaveCondition.timeWait || wave.nextWaveCondition == NextWaveCondition.waitTimeOrAllSpawnersReady)
            {
                //Attention!!!
                sum = wave.wavesConditionTime;
                //sum += wave.wavesConditionTime;
            }
            return sum;
        }
        float GetSpawnTimeInWave(int index)
        {
            Wave wave = waves[index];

            float sum = 0;
            int unitsTotalCount = 0;
            for (int i = 0; i < units.Count; i++)
            {
                unitsTotalCount += wave.unitsNum[i];
            }
            if (unitsTotalCount > 1)
            {
                sum += (unitsTotalCount - 1) * (wave.unitsDelay > minUnitsDelay ? wave.unitsDelay : minUnitsDelay);
            }
            sum += (wave.GroupsInWave - 1) * wave.groupsDelay;
            return sum;
        }
        public float GetPowerInWave(int index)
        {
            return GetWeightInWave(index) / GetSpawnTimeInWave(index);
        }

        public enum NextWaveCondition { allSpawnersReady, waitTimeOrAllSpawnersReady, timeWait, /*connectWaves,*/  externalEvent };
        public enum UnitsWeightMode { normal, multiplyByCoefficient};
//#if UNITY_EDITOR
        [System.Serializable]
        public class UnitRenderData
        {
            public bool isNeedSpace = false;
            public float unitWeightCoefficient = 1f;

        }
//#endif
    }
}