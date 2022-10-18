using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD.Data;
using MSFD;
using UnityEditor;
using System.IO;

[System.Serializable]
[CreateAssetMenu(fileName = "TE_ContainerCreator_", menuName = "DataSystem/TE_ContainerCreator")]
public class TE_ContainerCreator : TriggerEditorBase
{
    [SerializeField]
    D_Container_SO containerTemplate;
    [SerializeField]
    int totalCount = 10;

    public override void ActivateTriggerEditor(D_Container_SO container)
    {
#if UNITY_EDITOR
        //Debug.Log("Container works!");
        int currentCount = 0;
        string containerSampleName = containerTemplate.GetDataName();
        var childContainers = container.GetDisplayDataBases();
        foreach(var x in childContainers)
        {
            if (x!= null && x.GetDataName().Contains(containerSampleName)) 
            {
                currentCount++;
            }
        }
        string containerPath = AssetDatabase.GetAssetPath(container.GetInstanceID());
        string templatePath = AssetDatabase.GetAssetPath(containerTemplate.GetInstanceID());
        containerPath = containerPath.Substring(0,containerPath.LastIndexOf('/'));
        for (int i = currentCount; i < totalCount; i++)
        {
            string dirPath = containerPath + "/" + containerSampleName + i;
            if (!Directory.Exists(dirPath))
            {
                var dir = Directory.CreateDirectory(dirPath);
            }
            string absentAssetName = containerTemplate.name + i.ToString();
            string absentAssetPath = dirPath + "/" + absentAssetName + ".asset";
            D_Container_SO absentAsset = AssetDatabase.LoadAssetAtPath<D_Container_SO>(absentAssetPath);
            if (absentAsset == null)
            {
                AssetDatabase.CopyAsset(templatePath, absentAssetPath);
                absentAsset = AssetDatabase.LoadAssetAtPath<D_Container_SO>(absentAssetPath);
            }
            container.AddDisplayDataBase(absentAsset);
            absentAsset.SetDataName(containerSampleName + i);
            absentAsset.RemoveNoneReferenceDisplayDataBases();
            absentAsset.ActivateTriggerVerification();

            EditorUtility.SetDirty(container);
            EditorUtility.SetDirty(absentAsset);
        }
#endif
    }
}
