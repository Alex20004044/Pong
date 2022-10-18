using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    public class D_StringArray : DataBase
    {
        [TableList]
        [TabGroup("Values")]
        [SerializeField]
        List<DString> stringArray;
        public D_StringArray(string dataPath, DataType dataType) : base(dataPath, dataType)
        {
        }

        public string GetValue(string stringName)
        {
            foreach (DString x in stringArray)
            {
                if (x.name == stringName)
                {
                    return x.value;
                }
            }
            Debug.LogError("Array element " + stringName + " not found");
            return null;
        }
        public string GetValue(int index)
        {
            return stringArray[index].value;
        }
        public override void Load()
        {
            foreach (DString x in stringArray)
            {
                x.value = SaveCore.Load<string>(GetPath() + x.name, x.value);
            }
            OnValueChanged();
        }

        public override void Save()
        {
            foreach (DString x in stringArray)
            {
                SaveCore.Save(GetPath() + x.name, x.value);
            }
        }

        public bool TryAddValue(string stringName, string value)
        {
            if(GetValue(stringName) == null)
            {
                stringArray.Add(new DString(stringName, value));
                OnValueChanged();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TryRemoveValue(string stringName)
        {
            for (int i =0; i < stringArray.Count; i++)
            {
                if (stringArray[i].name == stringName)
                {
                    stringArray.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public bool TrySetValue(string stringName, string value)
        {
            for (int i = 0; i < stringArray.Count; i++)
            {
                if (stringArray[i].name == stringName)
                {
                    stringArray[i].value = value;
                    OnValueChanged();
                    return true;
                }
            }
            return false;
        }
        public override string GetDataDescription()
        {
            string description = "(" + stringArray.Count + ")";
            for (int i = 0; i < stringArray.Count; i++)
            {
                description += "\n\t" + i + ")" + stringArray[i].name + ": " + stringArray[i].value;
            }
            return description;
        }

        [System.Serializable]
        public class DString
        {
            public string name;
            public string value;

            public DString(string name, string value)
            {
                this.name = name;
                this.value = value;
            }
        }


    }
}