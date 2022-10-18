using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    public class DisplayController : DisplayBase
    {
        [SerializeField]
        DataSetter displayPrefab;
        [SerializeField]
        Transform parent;
        D_Container container;
        [SerializeField]
        string[] exeptData;

        public override void Display()
        {
            
        }

        //List<GameObject> displays = new List<GameObject>();
        public override void Initialize()
        {
            container = data as D_Container;

            List<DataBase> childData = container.GetDataBases();
            for(int i = 0; i < childData.Count; i++)
            {
                if(AuxiliarySystem.CompareTags(childData[i].Name, exeptData))
                {
                    continue;
                }
                GameObject display = Instantiate<GameObject>(displayPrefab.gameObject, parent);
                display.transform.localPosition = Vector3.zero;
                display.GetComponent<DataSetter>().InitData(data, childData[i].Name);
                display.name = display.name.Replace("Clone", childData[i].Name);
            }
        }
    }
}