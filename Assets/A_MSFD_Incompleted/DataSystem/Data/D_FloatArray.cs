using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace MSFD
{
    [System.Serializable]
    public class D_FloatArray : DataBase
    {
        [TableList]
        [TabGroup("Values")]
        [SerializeField]
        List<DFloat> floatArray;
        public D_FloatArray(string dataPath, DataType dataType) : base(dataPath, dataType)
        {
        }
        public float GetValue(string floatName)
        {
            foreach (DFloat x in floatArray)
            {
                if (x.name == floatName)
                {
                    return x.value;
                }
            }
            Debug.LogError("Array element " + floatName + " not found");
            return default(float);
        }
        public float GetValue(int index)
        {
            return floatArray[index].value;
        }

        public override void Load()
        {
            foreach (DFloat x in floatArray)
            {
                x.value = SaveCore.Load<float>(GetPath() + x.name, x.value);
            }
            OnValueChanged();
        }

        public override void Save()
        {
            foreach (DFloat x in floatArray)
            {
                SaveCore.Save(GetPath() + x.name, x.value);
            }
        }

        public bool TryAddValue(string name, float value)
        {
            if ((floatArray.Find((x) => x.name == name)) == null)
            {
                floatArray.Add(new DFloat(name, value));
                OnValueChanged();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryRemoveValue(int index)
        {
            for (int i =0; i < floatArray.Count; i++)
            {
                if (i == index)
                {
                    floatArray.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool TrySetValue(int index, float value)
        {
            for (int i = 0; i < floatArray.Count; i++)
            {
                if (i == index)
                {
                    floatArray[i].value = value;
                    OnValueChanged();
                    return true;
                }
            }
            return false;
        }
        public override string GetDataDescription()
        {
            string description = "(" + floatArray.Count + ")";
            for (int i = 0; i < floatArray.Count; i++)
            {
                description += "\n\t" + i + ")" + floatArray[i].name + ": " + floatArray[i].value;
            }
            return description;
        }
    }
    [System.Serializable]
    public class DFloat
    {
        public string name;
        public float value;

        public DFloat(string name, float value)
        {
            this.name = name;
            this.value = value;
        }
    }
}