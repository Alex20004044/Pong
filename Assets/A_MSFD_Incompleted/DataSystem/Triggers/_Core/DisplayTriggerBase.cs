using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    public abstract class DisplayTriggerBase : ScriptableObject
    {
        public abstract TriggerBase GetTriggerBase();
    }
}