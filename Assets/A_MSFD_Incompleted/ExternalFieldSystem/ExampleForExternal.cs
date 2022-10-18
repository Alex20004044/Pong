using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExampleForExternal : MonoBehaviour
{
    [SerializeField]
    public bool boolField = false;

    [ShowInInspector]
    public bool? refBoolField = true; 

    [Button]
    public bool BoolFunc()
    {
        return boolField;
    }


    public string stringField = "stringField";
    [Button]
    public string StingFunc()
    {
        return stringField;
    }
}
