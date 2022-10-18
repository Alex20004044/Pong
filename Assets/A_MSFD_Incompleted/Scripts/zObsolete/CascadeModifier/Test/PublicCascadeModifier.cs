using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
using System;
[Obsolete]
[Serializable]
public class PublicCascadeModifier<T>
{
    [SerializeField]
    CascadeModifier<T> cascadeModifier = new CascadeModifier<T>();
    public PublicCascadeModifier()
    {
    }
    public PublicCascadeModifier(T value)
    {
        cascadeModifier = new CascadeModifier<T>(value);
    }

    public void Add(Func<T, T> func, int priority = 0)
    {
        cascadeModifier.AddModifier(func, priority);
    }
    public void Remove(Func<T, T> func)
    {
        cascadeModifier.RemoveModifier(func);
    }
}
