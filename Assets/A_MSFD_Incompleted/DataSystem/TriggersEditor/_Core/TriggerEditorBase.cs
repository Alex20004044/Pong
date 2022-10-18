using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MSFD.Data
{

    [System.Serializable]
    public abstract class TriggerEditorBase : ScriptableObject
    {
        public abstract void ActivateTriggerEditor(D_Container_SO container);
    }
}