using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace MSFD.Data
{
    public class DisplayD_Text : DisplayBase
    {
        [Header("Works correctly with D_Float, D_String")]
        [SerializeField]
        TMP_Text textField;
        [SerializeField]
        string prefix = "";
        [SerializeField]
        string postfix = "";


        public override void Initialize()
        {

        }
        public override void Display()
        {
            textField.text = prefix + GetTextValue() + postfix;
        }

        string GetTextValue()
        {
            return data.GetDataDescription();
        }
        public void SetPrefix(string _prefix)
        {
            prefix = _prefix;
        }
        public string GetPrefix()
        {
            return prefix;
        }
        public void SetPostfix(string _postfix)
        {
            postfix = _postfix;
        }
        public string GetPostfix()
        {
            return postfix;
        }
    }
}