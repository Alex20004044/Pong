using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    [System.Serializable]
    public class T_CountChecker : TriggerBase
    {
        [SerializeField]
        int targetCount = 0;

        public T_CountChecker(int targetCount)
        {
            this.targetCount = targetCount;
        }

        public override void ActivateTrigger(D_Container container)
        {
            var dataBases = container.GetDataBases();
            if (dataBases.Count != targetCount)
            {
                Debug.LogError("There are incorrect data amount in container: " + container.GetDataName() + " (target count: " + targetCount + " | real count: " + dataBases.Count + ")");
            }
        }
    }
}
