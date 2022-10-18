using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "T_CountChecker_", menuName = "DataSystem/T_CountChecker")]
    public class T_CountChecker_SO : DisplayTriggerBase
    {
        [SerializeField]
        T_CountChecker trigger;
        public override TriggerBase GetTriggerBase()
        {
            return trigger;
        }
    }
}