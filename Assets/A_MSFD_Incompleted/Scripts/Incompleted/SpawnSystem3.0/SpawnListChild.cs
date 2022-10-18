using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
using UnityEditor;
namespace MSFD.SpawnSystem
{
    [CreateAssetMenu(fileName = "_SpawnListChild", menuName = "SpawnSystem/SpawnListChild")]
    [System.Serializable]
    public class SpawnListChild : SpawnList
    {
        public int[] mas;
        TestEnum testEnum = TestEnum.test;
#if UNITY_EDITOR
        public override void DrawUnitField(int ind, float unitFieldWidth)
        {
            units[ind] = (UnitDataChild)UnityEditor.EditorGUILayout.ObjectField("", units[ind], typeof(UnitDataChild),
            false, GUILayout.Width(unitFieldWidth));
        }
        public override void DrawWaveColumn(int ind, float width)
        {
            for (int j = 0; j < units.Count; j++)
            {
                testEnum = (TestEnum)EditorGUILayout.EnumFlagsField(testEnum, GUILayout.Width(width));
            }
        }
#endif
        enum TestEnum { test, single, second, plural, vegan}
    }

}
