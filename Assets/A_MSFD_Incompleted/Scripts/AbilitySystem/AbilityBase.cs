using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public abstract class AbilityBase : MonoBehaviour
    {
        [SerializeField]
        AbilityInteractionType abilityInteractionType = AbilityInteractionType.single;
        public abstract void Activate(GameObject _unit);
        public abstract void Deactivate();
        public AbilityInteractionType GetAbilityInteractionType()
        {
            return abilityInteractionType;
        }
        /// <summary>
        /// Single - only one instance of ability can be activated in the moment
        /// Plural - abilities don't interact with each other
        /// </summary>
        public enum AbilityInteractionType { single, plural};
    }
}