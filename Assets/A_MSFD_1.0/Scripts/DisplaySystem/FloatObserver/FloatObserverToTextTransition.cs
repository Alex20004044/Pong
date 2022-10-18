using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using System;
using UniRx;
namespace MSFD.UI
{
    public class FloatObserverToTextTransition : FieldObserverToBase<float>
    {
        [SerializeField]
        FloatObsTransitionCore delayedCore = new FloatObsTransitionCore();

        [SerializeField]
        TMP_Text text;
        [Header("Text before float value")]
        [SerializeField]
        string prefixText = string.Empty;
        [SerializeField]
        string postfixText = string.Empty;
        [SerializeField]
        string format = string.Empty;

        protected override void Awake()
        {
            base.Awake();
            delayedCore.Subscribe(Refresh);
        }

        public override void OnNext(float value)
        {
            delayedCore.OnNext(value);
        }

        void Refresh(float value)
        {
            text.text = prefixText + value.ToString(format) + postfixText;
        }
    }
}