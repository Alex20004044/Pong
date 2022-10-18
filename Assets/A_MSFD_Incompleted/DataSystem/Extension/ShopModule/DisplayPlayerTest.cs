using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace MSFD.Data
{
    public class DisplayPlayerTest : DisplayBase
    {
        [SerializeField]
        TMP_Text nameField;

        [SerializeField]
        TMP_Text speedField;
        [SerializeField]
        TMP_Text descriptionField;

        D_Float speedData;
        D_String descriptionData;

        public override void Initialize()
        {
            speedData = (data as D_Container).GetData<D_Float>(speedFieldName);
            descriptionData = (data as D_Container).GetData<D_String>(descriptionFieldName);
        }

        public override void Display()
        {
            nameField.text = data.GetDataName();
            speedField.text = speedData.GetValue().ToString();
            descriptionField.text = descriptionData.GetValue();
        }

        public static string speedFieldName = "Speed";
        public static string purchaseFieldName = "Purchase";
        public static string descriptionFieldName = "Description";
    }
}
