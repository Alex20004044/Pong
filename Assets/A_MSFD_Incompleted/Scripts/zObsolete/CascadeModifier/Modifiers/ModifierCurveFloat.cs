using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModifierCurveFloat : IModifier<float>
{
    [SerializeField]
    AnimationCurve curve = new AnimationCurve(
        new Keyframe(0, 0, 1, 1),
        new Keyframe(1, 1, 1, 1)
        );

    public Func<float, float> GetModifier()
    {
        return CurveTransform;
    }

    float CurveTransform(float value)
    {
        return curve.Evaluate(value);
    }
}
