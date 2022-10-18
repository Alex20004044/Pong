using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class ImpactData
    {
        GameObject sender;
        Dictionary<string, IImpact> impacts;

        public ImpactData(GameObject sender)
        {
            this.sender = sender;
        }
        /// <summary>
        /// This method rewrite previous impact if you add already existing in dictionary impact
        /// </summary>
        /// <param name="impact"></param>
        public void AddImpact(IImpact impact)
        {
            if (impacts.ContainsKey(impact.ImpactType))
            {
                impacts[impact.ImpactType] = impact;
            }
            else
                impacts.Add(impact.ImpactType, impact);
        }
        public IImpact GetImpact(string type)
        {
            return impacts[type];
        }
        public GameObject GetSender()
        {
            return sender;
        }
        public string[] GetImpactTypes()
        {
            string[] impactTypes = new string[impacts.Count];
            int i = 0;
            foreach(var x in impacts)
            {
                impactTypes[i] = x.Value.ImpactType;
                i++;
            }
            return impactTypes;
        }
    }
    public interface IImpact
    {
        string ImpactType { get; }
    }
    public class ImpactDamage : IImpact
    {
        public string ImpactType { get { return "Damage"; } }
        public float Damage { get; private set; }

        public ImpactDamage(float damage)
        {
            this.Damage = damage;
        }

    }
    public class ImpactImpulse : IImpact
    {
        public string ImpactType { get { return "Impulse"; } }
        public Vector3 Impulse { get; private set; }

        public ImpactImpulse(Vector3 impulse)
        {
            this.Impulse = impulse;
        }
    }
    public class ImpactFireDamage : IImpact
    {
        public ImpactFireDamage(float fireTime, float fireDamage)
        {
            FireTime = fireTime;
            FireDamage = fireDamage;
        }

        public string ImpactType { get { return "Fire"; } }
        public float FireTime { get; private set; }
        public float FireDamage { get; private set; }
    }
}