using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExternalFieldOdin<T> : ExternalFieldOdinBase
{
    public T GetValue()
    {
        return (T)GetRawValue();
    }
}
