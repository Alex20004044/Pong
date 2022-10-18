using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSetterTest : MonoBehaviour
{
    [SerializeField]
    FieldSetter fieldSetter;
    [SerializeField]
    Vector3 newValue = new Vector3(1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [Button]
    void Test()
    {
        fieldSetter.UpdateValue(newValue);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
