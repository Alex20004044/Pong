using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierFactorFloat", menuName = "Modifier/FactorFloat")]
[Serializable]
public class ModifierFactorFloat  : ScriptableObject, IModifier<float>
{
    [SerializeField]
    float factor = 0.5f;
    public Func<float, float> GetModifier()
    {
        return Factor;
    }

    float Factor(float value)
    {
        return value * factor;
    }
}
