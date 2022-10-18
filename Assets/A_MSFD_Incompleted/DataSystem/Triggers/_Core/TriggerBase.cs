using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    [System.Serializable]
    public abstract class TriggerBase
    {
        public abstract void ActivateTrigger(D_Container container);
    }
}
