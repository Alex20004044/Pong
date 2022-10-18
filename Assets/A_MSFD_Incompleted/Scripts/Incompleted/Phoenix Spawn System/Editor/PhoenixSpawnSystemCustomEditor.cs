using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace MSFD.PhoenixSpawnSystem
{
    public class AssetHandler
    {
        [OnOpenAsset()]
        public static bool OpenEditor(int instanceId, int line)
        {
            SpawnList obj = EditorUtility.InstanceIDToObject(instanceId) as SpawnList;
            if (obj != null)
            {
                PhoenixSpawnListEditorWindow.Open(obj);
                return true;
            }
            return false;
        }
    }

    [CustomEditor(typeof(SpawnList))]
    public class PhoenixSpawnSystemCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Phoenix Spawn List"))
            {
                PhoenixSpawnListEditorWindow.Open((MSFD.PhoenixSpawnSystem.SpawnList)target);
            }
        }
    }
}
