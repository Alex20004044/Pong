using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace MSFD.Data
{
    public class DisplayD_String_Dropdown : DisplayBase
    {
        [SerializeField]
        TMP_Dropdown dropdown;

        [SerializeField]
        InitMode initMode = InitMode.useExistingDropdownOptions;
        [Sirenix.OdinInspector.ShowIf("@initMode == InitMode.setDropdownOptionsFromScript")]
        [SerializeField]
        List<string> dropdownOptions;

        D_String d_String;
        public override void Initialize()
        {
            d_String = data as D_String;
            if (initMode == InitMode.setDropdownOptionsFromScript)
            {
                dropdown.ClearOptions();
                dropdown.AddOptions(dropdownOptions);
            }
            dropdown.onValueChanged.AddListener(OnValueChanged);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            dropdown.onValueChanged.RemoveListener(OnValueChanged);
        }
        public override void Display()
        {
            int index = dropdown.options.FindIndex((x) => x.text == d_String.GetValue());
            if(index == -1)
            {
                Debug.LogError("Incorrect default value in field: " + data.GetPath() + ". Display D_String_Dropdown " + name + " can't define value");
            }
            if(dropdown.value != index)
            {
                dropdown.value = index;
            }
        }
        void OnValueChanged(int index)
        {
            if(d_String.GetValue() == dropdown.options[index].text)
            {
                return;
            }
            else
            {
                d_String.SetValue(dropdown.options[index].text);
            }
        }
        public enum InitMode {useExistingDropdownOptions, setDropdownOptionsFromScript };
    }
}