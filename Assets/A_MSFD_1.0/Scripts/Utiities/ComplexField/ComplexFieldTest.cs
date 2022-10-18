using MSFD;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ComplexFieldTest : MonoBehaviour
{
    [SerializeField]
    ComponentField<Collider> complexFieldGo;
    [SerializeField]
    ComponentField<Collider> componentTransfrom;

    [SerializeField]
    DSField<D_Float> floatTest;
    [SerializeField]
    DSField<D_Float_SO> floatTestSO;
    private void Awake()
    {
        componentTransfrom.Initialize();
    }
    [Button]
    private void Initialize()
    {
        componentTransfrom.Initialize();
    }

    [Button]
    public Collider GetCT()
    {
        return componentTransfrom.V;
    }
}
