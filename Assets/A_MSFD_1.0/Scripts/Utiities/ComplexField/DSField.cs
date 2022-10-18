using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MSFD;
using System;

[System.Serializable]
public class DSField<T> : ComplexFieldBase<T> where T : class
{
    [OnValueChanged("@" + nameof(OnChangeInitializationMode) + "()")]
    [SerializeField]
    DataInitializationMode dataInitializationMode = DataInitializationMode.dataSystem;

    [ShowIf("@" + nameof(IsShowPath) + "()")]
    [SerializeField]
    string path;


    public override void Initialize()
    {
        if (dataInitializationMode == DataInitializationMode.dataSystem)
        {
            value = DC.GetData<T>(path);
        }
    }

   

    void OnChangeInitializationMode()
    {
        isShowValueInInspector = false;
        switch (dataInitializationMode)
        {
            case DataInitializationMode.manual:
                isShowValueInInspector = true;
                break;
            case DataInitializationMode.dataSystem:
                break;
            case DataInitializationMode.external:
                break;
            default:
                break;
        }
    }
    bool IsShowPath()
    {
        return dataInitializationMode == DataInitializationMode.dataSystem;
    }
    enum DataInitializationMode { manual, dataSystem, external }
}
