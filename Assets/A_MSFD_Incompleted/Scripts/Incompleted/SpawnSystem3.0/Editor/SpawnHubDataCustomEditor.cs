using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using MSFD;
namespace MSFD.SpawnSystem
{
    public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        SpawnHubData obj = EditorUtility.InstanceIDToObject(instanceId) as SpawnHubData;
        if(obj != null)
        {
            SpawnHubDataEditorWindow.Open(obj);
            return true;
        }
        return false;
    }
}


    [CustomEditor(typeof(SpawnHubData))]
    public class SpawnHubDataCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if(GUILayout.Button("Open Spawn Hub"))
            {
                SpawnHubDataEditorWindow.Open((MSFD.SpawnSystem.SpawnHubData)target);
            }
        }
    }
}