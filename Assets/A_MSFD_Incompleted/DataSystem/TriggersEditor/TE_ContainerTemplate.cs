using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
namespace MSFD.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "TE_ContainerTemplate_", menuName = "DataSystem/TE_ContainerTemplate")]
    public class TE_ContainerTemplate : TriggerEditorBase
    {
        //[("Template")]
        [SerializeField]
        D_Container_SO template;
        public override void ActivateTriggerEditor(D_Container_SO container)
        {
#if UNITY_EDITOR
            var templateDataBases = template.GetDisplayDataBases();
            var displayDataBases = container.GetDisplayDataBases();
            foreach (DisplayDataBase templateDataBase in templateDataBases)
            {
                if (!displayDataBases.Find((x) => { return templateDataBase.GetDataName() == x.GetDataName(); }))
                {
                    
                    string containerPath = AssetDatabase.GetAssetPath(container.GetInstanceID());
                    string templateDataPath = AssetDatabase.GetAssetPath(templateDataBase.GetInstanceID());
                    string dataPath = containerPath.Substring(0, containerPath.LastIndexOf("/")) + "/" + templateDataBase.name + ".asset";//containerPath.Substring( containerPath.LastIndexOf(".");

                    DisplayDataBase absentAsset;
                    absentAsset = AssetDatabase.LoadAssetAtPath<DisplayDataBase>(dataPath);
                    if (absentAsset == null)
                    {
                        AssetDatabase.CopyAsset(templateDataPath, dataPath);
                        absentAsset = AssetDatabase.LoadAssetAtPath<DisplayDataBase>(dataPath);
                    }
                    container.AddDisplayDataBase(absentAsset);
                    EditorUtility.SetDirty(container);
                    Debug.LogError($"TE_ContainerTemplate: Data {templateDataBase.GetDataName()} is not found in " + containerPath);
                }
            }
#endif
        }
    }
}