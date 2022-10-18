using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
namespace MSFD.Data
{
    public class DisplayD_Float_Slider : DisplayBase
    {
        [SerializeField]
        Slider slider;
        [SerializeField]
        InitMode sliderWorkMode = InitMode.standartSliderParametres;
        [HideIfGroup("DisableStandart", Condition = "@sliderWorkMode == SliderWorkMode.mapDiscreteValues || sliderWorkMode == SliderWorkMode.onlySliderRestriction")]
        [SerializeField]
        float minValue = 0;
        [HideIfGroup("DisableStandart")]
        [SerializeField]
        float maxValue = 1;
        [HideIfGroup("DisableStandart")]
        [SerializeField]
        bool isWholeNumbers = true;

        [ShowIfGroup("MapCurveFunction", Condition = "@sliderWorkMode == SliderWorkMode.mapCurveFunction")]
        [Header("Works only with linear functions")]
        [SerializeField]
        AnimationCurve mapCurve;
        [ShowIfGroup("MapCurveFunction")]
        [SerializeField]
        AnimationCurve inverseMapCurve;
        [ShowIfGroup("MapCurveFunction")]
        [Header("In is need for correct inversing from curve")]
        [SerializeField]
        float treshold = 0.1f;

        [ShowIfGroup("MapDiscreteValues", Condition = "@sliderWorkMode == SliderWorkMode.mapDiscreteValues")]
        [SerializeField]
        float[] mapValues = new float[2];

        D_Float dFloat;
        public override void Initialize()
        {
            dFloat = data as D_Float;
            slider.onValueChanged.AddListener(OnSliderValueChanged);

            switch (sliderWorkMode)
            {
                case InitMode.standartSliderParametres:
                    slider.minValue = minValue;
                    slider.maxValue = maxValue;
                    slider.wholeNumbers = isWholeNumbers;
                    break;
                case InitMode.onlySliderRestriction:
                    break;
                case InitMode.mapCurveFunction:
                    slider.minValue = minValue;
                    slider.maxValue = maxValue;
                    slider.wholeNumbers = isWholeNumbers;
                    inverseMapCurve = GetInverseCurve(mapCurve);                    
                    break;
                case InitMode.mapDiscreteValues:
                    slider.wholeNumbers = true;
                    slider.minValue = 0;
                    slider.maxValue = mapValues.Length - 1;
                    break;
                default:
                    break;
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
        public override void Display()
        {
            float targetSliderValue;
            if (sliderWorkMode == InitMode.mapCurveFunction)
            {
                targetSliderValue = inverseMapCurve.Evaluate(dFloat.GetValue());
                if (Mathf.Abs((slider.value - targetSliderValue)) <= treshold)
                    return;
            }
            else if(sliderWorkMode == InitMode.mapDiscreteValues)
            {
                targetSliderValue = slider.value;
                bool isCorrect = false;
                float f = dFloat.GetValue();
                for (int i = 0; i < mapValues.Length; i++)
                {
                    if(f == mapValues[i])
                    {
                        targetSliderValue = i;
                        isCorrect = true;
                        break;
                    }
                }
                if (!isCorrect)
                    Debug.LogError("Incorrect default value in field: " + data.GetPath() + ". Display D_Float_Slider " + name + " can't define value");
            }
            else
            {
                targetSliderValue = dFloat.GetValue();
            }

            if (slider.value == targetSliderValue)
            {
                return;
            }
            else
            {
                slider.value = targetSliderValue;
            }
        }
        void OnSliderValueChanged(float value)
        {
            if(sliderWorkMode == InitMode.mapCurveFunction)
            {
                value = mapCurve.Evaluate(value);
            }
            else if(sliderWorkMode == InitMode.mapDiscreteValues)
            {
                value = mapValues[(int)value];
            }
            if (dFloat.GetValue() != value)
            {
                dFloat.SetValue(value);
            }
        }
        AnimationCurve GetInverseCurve(AnimationCurve sourceCurve)
        {
            AnimationCurve inverseCurve = new AnimationCurve();
            for (int i = 0; i < sourceCurve.length; i++)
            {
                Keyframe sourceKey = sourceCurve[i];
                Keyframe inverseKey = new Keyframe(sourceKey.value, sourceKey.time);
                inverseKey.tangentMode = sourceKey.tangentMode;
                inverseKey.inTangent = Mathf.Tan(Mathf.PI/2 - Mathf.Atan(sourceKey.inTangent));
                inverseKey.outTangent = Mathf.Tan(Mathf.PI / 2 - Mathf.Atan(sourceKey.outTangent));

                inverseKey.weightedMode = sourceKey.weightedMode;
                inverseKey.inWeight = sourceKey.inWeight;
                inverseKey.outWeight = sourceKey.outWeight;

                //, sourceCurve.keys[i].outTangent, sourceCurve.keys[i].inTangent, sourceCurve.keys[i].outWeight, sourceCurve.keys[i].inWeight);
                inverseCurve.AddKey(inverseKey);
            }
            return inverseCurve;
        }
        public enum InitMode { standartSliderParametres, onlySliderRestriction, mapCurveFunction, mapDiscreteValues };
    }
}