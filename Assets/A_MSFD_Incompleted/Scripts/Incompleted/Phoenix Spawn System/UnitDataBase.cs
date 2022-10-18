using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    [System.Serializable]
    public abstract class UnitDataBase : ScriptableObject
    {
        [SerializeField]
        GameObject prefab;

        [SerializeField]
        string[] tags;
        public virtual string[] GetTags()
        {
            return tags;
        }
        public virtual float GetWeight()
        {
            return 1;
        }
        public virtual string GetDescription()
        {
            string s = "";
            if (s != null)
            {
                foreach (string x in tags)
                {
                    s += x + ",";
                }
                s = s.Remove(s.Length - 1);
            }
            return s;
        }
        public GameObject GetPrefab()
        {
            return prefab;
        }
    }
    
}