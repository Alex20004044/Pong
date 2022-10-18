using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "_ModifierCurveFloat", menuName = "Modifier/CurveFloat")]
[Serializable]
public class ModifierCurveFloatSO : ScriptableObject, IModifier<float>
{
    [SerializeField]
    ModifierCurveFloat modifier;

    public Func<float, float> GetModifier()
    {
        return modifier.GetModifier();
    }
}
