using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class BonusEmpty : BonusBase
    {
#if UNITY_EDITOR
        [SerializeField]
        string desctiption;
#endif
        protected override void ActivateBonus()
        {
        }
        protected override void DeactivateBonus()
        {
        }
    }
}
