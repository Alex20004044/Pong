using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
namespace MSFD.Data
{
    public class DataSetter : MonoBehaviour
    {
        [SerializeField]
        InitializationMode initializationMode = InitializationMode.manual;
        [SerializeField]
        string relativePath;

        [SerializeField]
        List<DataSetter> childDataSetters = new List<DataSetter>();

        [SerializeField]
        UnityEvent onInitData = new UnityEvent();

        protected DataBase data;
        private void Start()
        {
            if (initializationMode == InitializationMode.manual)
            {
                data = DC.GetData(relativePath);
                InitChildrenDataSet();
                onInitData.Invoke();
            }
        }
        public void InitData(DataBase _data, string _relativePath)
        {
            SetRelativePath(_relativePath);
            InitData(_data);
        }
        public void InitData(DataBase _data)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                data = _data;
            }
            else
            {
                data = ((D_Container)_data).GetData(relativePath);
            }
            InitChildrenDataSet();
            onInitData.Invoke();
        }
        void InitChildrenDataSet()
        {
            foreach (DataSetter x in childDataSetters)
            {
                if (x.initializationMode != InitializationMode.manual)
                    x.InitData(data);
                else
                    Debug.LogError("Attempt to Init Data in manual mode");
            }
        }

        [Button]
        public DataBase GetData()
        {
            return data;
        }
        public void SetRelativePath(string _relativePath)
        {
            relativePath = _relativePath;
        }
        public void AddChildDataSetter(DataSetter dataSetter)
        {
            childDataSetters.Add(dataSetter);
        }
        public void RemoveChildDataSetter(DataSetter dataSetter)
        {
            childDataSetters.Remove(dataSetter);
        }
        public string GetPath()
        {
            return data.GetPath();
        }
        public enum InitializationMode { manual, external };
    }
    /*
    [System.Serializable]
    public class DataSet
    {
        
        [HorizontalGroup]
        public string dataPath = "";
        [HorizontalGroup]
        public DataSetter dataSetter;

        public void InitDataSetter(DataBase data)
        {
            if(string.IsNullOrEmpty(dataPath))
            {
                dataSetter.InitData(data);
            }
            else
            {
                dataSetter.InitData(((D_Container)data).GetData(dataPath));
            }
        }
    }*/
}