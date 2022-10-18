using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
using MSFD.Data;
public class TestCont : MonoBehaviour
{
    // Start is called before the first frame update
    D_Container_SO D_Container_SO;
    void Start()
    {
        D_Container container = new D_Container("Container", DataType.readWrite);
        container.AddData(new D_Float("FLOAT", DataType.readWrite));
        container.AddData(container);
        container.TriggerProcessor.AddTrigger(new T_CountChecker(1));
        container.TriggerProcessor.ActivateTriggerVerification(container);
        Debug.Log(container.GetDataDescription());
        container.TryRemoveData("FLOAT");
        container.TriggerProcessor.ActivateTriggerVerification(container);
        Debug.Log(container.GetDataDescription());


        container.SaveContainerRecoursively();
        container.OnValueChangedContainerRecoursively();

        container.AddData(D_Container_SO.GetDataBase());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
