using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public abstract class SingletoneBase<T> : MonoBehaviour where T : class
    {
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    Debug.LogError("There is no " + typeof(T) + " in current scene. Add " + typeof(T) + " to current scene to get correct access");

                    Debug.LogError("Autocreation of " + typeof(T) + "...");
                    GameObject go = new GameObject(typeof(T).Name + "_AUTOCREATED");
                    go.AddComponent(typeof(T));
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        static T _instance;
        /// <summary>
        /// Don't forget call base.Awake() on override!
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
                AwakeInitialization();
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
        /// <summary>
        /// Use it instead of Awake
        /// </summary>
        protected virtual void AwakeInitialization()
        {

        }
    }
}