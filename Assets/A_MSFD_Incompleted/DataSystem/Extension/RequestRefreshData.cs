using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    public class RequestRefreshData : MonoBehaviour
    {
        [Header("Send request to data. Can be used to rebroadcast OnValueChanged")]
        [SerializeField]
        string[] dataPaths;

        [SerializeField]
        ActivationType activationType = ActivationType.onEnable;
        private void OnEnable()
        {
            if (activationType == ActivationType.onEnable)
            {
                SendRequest();
            }
        }
        public void SendRequest()
        {
            foreach (string x in dataPaths)
            {
                DC.GetData(x).OnValueChanged();
            }
        }
        enum ActivationType { onEnable, manual };
    }
}