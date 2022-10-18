using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FlagField<T>
{
    [SerializeField]
    public T v;
    [SerializeField]
    bool flag;


    public static implicit operator T(FlagField<T> flagField)
    {
        return flagField.v;
    }

    public bool GetFlag()
    {
        return flag;
    }
    public void SetFlag(bool _flag)
    {
        flag = _flag;
    }
}
