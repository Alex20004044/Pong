using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public abstract class SkillBase : AbilityBase
    {
        protected GameObject unit;
        public override void Activate(GameObject _unit)
        {
            unit = _unit;
            ActivateSkill();
        }

        public override void Deactivate()
        {
            DeactivateSkill();
        }
        protected abstract void ActivateSkill();
        protected abstract void DeactivateSkill();
    }
}
