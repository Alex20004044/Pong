using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public abstract class InfluenceCore : MonoBehaviour
    {
        protected string tag;
        public CascadeModifier<Influence> cascadeModifier;

        public void Impact(Influence influence)
        {
            cascadeModifier.CalculateWithModifiers(influence);
        }
    }
}