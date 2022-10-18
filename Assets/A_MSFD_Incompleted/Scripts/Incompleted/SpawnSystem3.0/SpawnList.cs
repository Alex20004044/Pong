using MSFD.CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MSFD.SpawnSystem
{
    [CreateAssetMenu(fileName = "_SpawnList", menuName = "SpawnSystem/SpawnList")]
    [System.Serializable]
    public class SpawnList : SpawnListBase
    {
        public List<UnitWave> waves = new List<UnitWave>();

#if UNITY_EDITOR
        public string groupsInWaveName = "Groups in Wave: ";
        public string groupsDelayName = "Groups Delay: ";
        public string unitsDelayName = "Units Delay: ";
        public string nextWaveConditionName = "Next Wave Condition: ";


        public override void AddUnitDataField()
        {
            units.Add(null);
            for(int i = 0 ; i < waves.Count; i++)
            {
                waves[i].AddUnitCell();
            }
        }
        public override bool RemoveUnitDataField()
        {
            if (units.Count > 0)
            {
                units.RemoveAt(units.Count - 1);
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
        public override void AddWave()
        {
            waves.Add(new UnitWave(units.Count));
        }
        public override bool RemoveWave()
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
        public override int GetWavesCount()
        {
            return waves.Count;
        }
        public override void DrawUnitField(int ind, float unitFieldWidth)
        {
            units[ind] = (UnitDataBase)UnityEditor.EditorGUILayout.ObjectField("", units[ind], typeof(UnitDataBase),
            false, GUILayout.Width(unitFieldWidth));
        }
        public override void DrawUnderUnitField(float unitFieldWidth)
        {
            EditorGUILayout.LabelField(groupsInWaveName, GUILayout.Width(unitFieldWidth));
            EditorGUILayout.LabelField(groupsDelayName, GUILayout.Width(unitFieldWidth));
            EditorGUILayout.LabelField(unitsDelayName, GUILayout.Width(unitFieldWidth));

            EditorGUILayout.LabelField(nextWaveConditionName, GUILayout.Width(unitFieldWidth));
        }
        public override void DrawWaveColumn(int ind, float width)
        {
            for (int j = 0; j < units.Count; j++)
            {
                waves[ind].unitsNum[j] = EditorGUILayout.IntField(waves[ind].unitsNum[j], GUILayout.Width(width));
            }
        }
        public override void DrawUnderWaveColumn(int ind, float width)
        {
            waves[ind].groupsInWave = EditorGUILayout.IntField(waves[ind].groupsInWave, GUILayout.Width(width));
            waves[ind].groupsDelay = EditorGUILayout.FloatField(waves[ind].groupsDelay, GUILayout.Width(width));
            waves[ind].unitsDelay = EditorGUILayout.FloatField(waves[ind].unitsDelay, GUILayout.Width(width));

            waves[ind].nextWaveCondition = (NextWaveCondition) EditorGUILayout.EnumPopup(waves[ind].nextWaveCondition, GUILayout.Width(width));
        }

        public override float GetTotalWeight()
        {
            float sum =0;
            for(int i =0; i <waves.Count; i++)
            {
                sum += GetWeightInWave(i);
            }
            return sum;
        }
        public override float GetTotalTime()
        {
            float sum = 0;
            for (int i = 0; i < waves.Count; i++)
            {
                sum += GetTimeInWave(i);
            }
            return sum;
        }
        public override float GetWeightInWave(int index)
        {
            return index * Random.Range(0, 100);
        }
        public override float GetTimeInWave(int index)
        {
            return index * Random.Range(0, 10);
        }


#endif
        public enum NextWaveCondition { allUnitsDead, waitTimeOrUnitsDeath, externalEvent };
    }
}