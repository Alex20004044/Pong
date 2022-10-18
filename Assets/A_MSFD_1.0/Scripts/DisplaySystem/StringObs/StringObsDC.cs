using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [System.Serializable]
    public class StringObsDC : FieldObsDCBase<string>
    {
        [SerializeField]
        DisplayMode displayMode = DisplayMode.value;

        protected override void OnDataNext(IData data)
        {
            string value;
            switch (displayMode)
            {
                case DisplayMode.value:
                    value = ((D_String)data).GetValue();
                    break;
                case DisplayMode.textView:
                    value = data.GetTextView();
                    break;
                case DisplayMode.path:
                    value = data.GetPath();
                    break;
                case DisplayMode.name:
                    value = data.Name;
                    break;
                default:
                    value = "Unknown DisplayMode: " + displayMode;
                    break;
            }
            OnNext(value);
        }

        public enum DisplayMode { value, textView, path, name };
    }
}