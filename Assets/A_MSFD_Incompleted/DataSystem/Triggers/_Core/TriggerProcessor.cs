using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD.Data
{
    [System.Serializable]
    public class TriggerProcessor
    {
        List<TriggerBase> triggers = new List<TriggerBase>();

        public void ActivateTriggerVerification(D_Container container)
        {
            foreach(TriggerBase x in triggers)
            {
                x.ActivateTrigger(container);
            }
        }
        public void AddTrigger(TriggerBase trigger)
        {
            triggers.Add(trigger);
        }
        public void InitializeTriggers(List<DisplayTriggerBase> displayTriggerBases)
        {
            triggers = new List<TriggerBase>();
            foreach(DisplayTriggerBase x in displayTriggerBases)
            {
                triggers.Add(x.GetTriggerBase());
            }
        }
    }
}