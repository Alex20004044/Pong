using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class CurveTest : MonoBehaviour
{
    [SerializeField]
    AnimationCurve animationCurve;

    [SerializeField]
    float inTangent = 1f;
    [SerializeField]
    float outTangent = 1f;
    [SerializeField]
    float inWeight = 1f;
    [SerializeField]
    float outWeight = 1f;

    [SerializeField]
    float rinTangent = 1f;
    [SerializeField]
    float routTangent = 1f;
    [SerializeField]
    float rinWeight = 1f;
    [SerializeField]
    float routWeight = 1f;
    [SerializeField]
    [Button]
    public void SetParams()
    {
        Keyframe keyframe = animationCurve.keys[1];
        keyframe.inTangent = inTangent;
        keyframe.outTangent = outTangent;
        keyframe.inWeight = inWeight;
        keyframe.outWeight = outWeight;
        animationCurve.RemoveKey(1);
        animationCurve.AddKey(keyframe);
        //animationCurve.keys[1] = keyframe;
    }

    private void Update()
    {
        Keyframe keyframe = animationCurve.keys[1];

        rinTangent = keyframe.inTangent;
        routTangent = keyframe.outTangent;
        rinWeight = keyframe.inWeight;
        routWeight = keyframe.outWeight;
    }
}
