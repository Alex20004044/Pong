using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalFieldTest : MonoBehaviour
{
    /*public ExternalField1<float> externalField;
    public ExternalField1<Transform> transformField;
    public ExternalField1<object> objectField;
    public ExternalField1<string> stringField;
    [SerializeField]
    float testFloat;*/
    //public ExternalField1<bool> boolField;
    public ExternalField1<string> stringField1;
    public ExternalFieldOdinBase externalFieldOdinBase;
    public ExternalField stringField;
    /*public ExternalFieldOdin<float> externalFieldOdin;*/

    private void Awake()
    {
        stringField1.GetValue();
    }
}
