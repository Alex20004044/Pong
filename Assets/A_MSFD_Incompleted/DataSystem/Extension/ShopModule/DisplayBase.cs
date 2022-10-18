using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD.Data
{
    [RequireComponent(typeof(DataSetter))]
    public abstract class DisplayBase : MonoBehaviour
    {
        [SerializeField]
        InitializationMode initializationMode = InitializationMode.getFromDataSetter;
        [HideIf("@initializationMode == InitializationMode.getFromDataSetter")]
        [SerializeField]
        string path;

        protected DataBase data;
        DataSetter dataSetter;
        private void Start()
        {
            if(initializationMode == InitializationMode.manual)
            {
                data = DC.GetData(path);
                Initialize();
                Display();
            }
            else
            {
                dataSetter = GetComponent<DataSetter>();
                Invoke(nameof(InitData), 0);
            }

        }

        void InitData()
        {
            data = dataSetter.GetData();
            data.AddListenerOnValueChanged(Display);
            Initialize();
            Display();
        }
        protected virtual void OnDestroy()
        {
            data.RemoveListenerOnValueChanged(Display);
        }
        [Button]
        public abstract void Initialize();
        [Button]
        public abstract void Display();

        public enum InitializationMode { getFromDataSetter, manual };
    }
}
