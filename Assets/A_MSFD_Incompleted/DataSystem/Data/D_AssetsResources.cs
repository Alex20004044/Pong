using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD.Data;
using Sirenix.OdinInspector;
namespace MSFD
{
    [System.Serializable]
    public class D_AssetsResources : DataBase
    {
        [TableList]
        [TabGroup("Values")]
        [SerializeField]
        List<Asset> assets;

        public D_AssetsResources(string dataPath, DataType dataType) : base(dataPath, dataType)
        {
        }

        public object GetAsset(string assetName)
        {
            foreach (Asset x in assets)
            {
                if (x.name == assetName)
                {
                    return x.GetAsset();
                }
            }
            Debug.LogError("Array element " + assetName + " not found");
            return null;
        }
        public T GetAsset<T>(string assetName)
        {
            return (T)GetAsset(assetName);
        }
        public object GetAsset(int index)
        {
            return assets[index];
        }
        public T GetAsset<T>(int index)
        {
            return (T)GetAsset(index);
        }
        public override string GetDataDescription()
        {
            string description = "(" + assets.Count + ")";
            for (int i = 0; i < assets.Count; i++)
            {
                description += "\n\t" + i + ")" + assets[i].name + ": " + assets[i].assetPath;
            }
            return description;
        }
        /// <summary>
        /// Unload asset with help of Resources.UnloadAsset
        /// </summary>
        /// <param name="assetName"></param>
        public void UnloadAsset(string assetName)
        {
            foreach (Asset x in assets)
            {
                if (x.name == assetName)
                {
                    Resources.UnloadAsset(x.GetAsset() as Object);
                    return;
                }
            }
            Debug.LogError("Attempt to unload not existed asset");
        }
        public static void UnloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
        }

        [System.Serializable]
        public struct Asset
        {
            [HideLabel]
            public string name;
            [HideLabel]
            public string assetPath;
            public object GetAsset()
            {
                return Resources.Load(assetPath);
            }
        }
    }
}
