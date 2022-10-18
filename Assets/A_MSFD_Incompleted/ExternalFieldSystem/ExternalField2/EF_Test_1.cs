using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
public class EF_Test_1 : MonoBehaviour
{
    [SerializeField]
    Transform[] transforms;
    [SerializeField]
    List<Transform> transformList;
    public Transform[] GetTransforms()
    {
        return transforms;
    }
    [SerializeField]
    ExternalField<float> externalFieldFloat;


    [SerializeField]
    ExternalField<int> externalFieldInt;
    [SerializeField]
    ExternalField<string> externalFieldString;
    [SerializeField]
    ExternalField<Component> externalFieldComponent;

    [SerializeField]
    ExternalField<List<Transform>> targets;

    [SerializeField]
    ExternalField<Transform[]> transformsArrayField;

    [SerializeField]
    ExternalField<float> speed;
    private void Start()
    {
        externalFieldInt.GetValue();
        
    }
}
