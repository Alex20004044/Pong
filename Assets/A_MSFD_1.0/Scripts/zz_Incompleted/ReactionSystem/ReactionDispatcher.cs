using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class ReactionDispatcher : MonoBehaviour, IReactable
    {
        Dictionary<string, List<Action<ImpactData>>> impactListeners;
        public void Impact(ImpactData impactData)
        {
            string[] impactTypes = impactData.GetImpactTypes();
            List<Action<ImpactData>> currentImpactListeners;
            foreach (var impactType in impactTypes)
            {
                if(impactListeners.TryGetValue(impactType, out currentImpactListeners))
                {
                    foreach(var x in currentImpactListeners)
                    {
                        x.Invoke(impactData);
                    }
                }
            }
        }

        public void AddListener(string impactType, Action<ImpactData> callback)
        {
            List<Action<ImpactData>> currentImpactListeners;
            if (impactListeners.TryGetValue(impactType, out currentImpactListeners))
            {
                currentImpactListeners.Add(callback);
            }
            else
            {
                currentImpactListeners = new List<Action<ImpactData>>();
                impactListeners.Add(impactType, currentImpactListeners);
            }
            currentImpactListeners.Add(callback);
        }

        public void RemoveListener(string impactType, Action<ImpactData> callback)
        {
            throw new NotImplementedException();
        }
    }
}