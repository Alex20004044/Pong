using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    public class FloatObsDC : FieldObsDCBase<float>
    {
        [Header("Transform value from one range to another")]
        [SerializeField]
        bool isMapRecievedValue = false;
        [ShowIf("$" + nameof(isMapRecievedValue))]
        [SerializeField]
        Vector2 inputRange = new Vector2(0, 100);
        [ShowIf("$" + nameof(isMapRecievedValue))]
        [SerializeField]
        Vector2 outputRange = new Vector2(0, 1);
        protected override void OnDataNext(IData data)
        {
            OnNext(((D_Float)data).GetValue());
        }
        public override void OnNext(float value)
        {
            if (isMapRecievedValue)
                value = AS.Calculation.Map(value, inputRange, outputRange);
            base.OnNext(value);
        }
    }
}