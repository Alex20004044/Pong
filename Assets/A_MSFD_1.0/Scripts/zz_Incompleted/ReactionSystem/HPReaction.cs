using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
using System;

public class HPReaction : MonoBehaviour
{
    IReactable reactable;
    IHP hp;
    private void Awake()
    {
        reactable.AddListener("Damage", OnDamage);
        reactable.AddListener("Impulse", OnImpulse);
    }

    private void OnImpulse(ImpactData impactData)
    {
        ImpactImpulse impactDamage = impactData.GetImpact("Impulse") as ImpactImpulse;
        hp.GetHP().Increase(impactDamage.Impulse.magnitude);
    }

    void OnDamage(ImpactData impactData)
    {
        ImpactDamage impactDamage = impactData.GetImpact("Damage") as ImpactDamage;
        Debug.Log(impactData.GetSender().name + " is damaged " + gameObject.name + " " + impactDamage.Damage);
        hp.GetHP().Decrease(impactDamage.Damage);
    }
}
