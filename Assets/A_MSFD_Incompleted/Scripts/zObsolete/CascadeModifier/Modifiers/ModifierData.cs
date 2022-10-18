using MSFD;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ModifierData<T>
{
    [HorizontalGroup(nameof(ModifierData<T>))]
    [SerializeField]
    InterfaceField<IModifier<T>> modifier;
    [HorizontalGroup(nameof(ModifierData<T>))]
    [SerializeField]
    int priority = 0;

    public IModifier<T> GetIModifier()
    {
        return modifier.i;
    }
    public int GetPriority()
    {
        return priority;
    }
}
