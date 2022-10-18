using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
public class CascadeModifierTest : MonoBehaviour
{
    [SerializeField]
    CascadeModifier<float> test = new CascadeModifier<float>(100);
    private void Awake()
    {

        test.AddModifier(Half, 0);
        test.AddModifier(TenPlus, -100);

    }
    private void OnDestroy()
    {


        test.RemoveModifier(Half, 0);
        test.RemoveModifier(TenPlus, -100);

    }
    [Sirenix.OdinInspector.Button]
    void Debugsss()
    {
        Debug.Log((float)test.GetValue());
    }

    float Half(float value)
    {
        return value * 0.5f;
    }
    float TenPlus(float value)
    {
        return value + 10;
    }
    
}
