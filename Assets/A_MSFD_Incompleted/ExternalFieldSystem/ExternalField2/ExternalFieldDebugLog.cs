using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
using Sirenix.OdinInspector;

public class ExternalFieldDebugLog : MonoBehaviour
{
    [SerializeField]
    ExternalField<string> someField;
    private void Start()
    {
        someField.Initialize();
    }
    [Button]
    public void Activate()
    {
        Debug.LogError(someField.GetValue().ToString());
    }
} 
