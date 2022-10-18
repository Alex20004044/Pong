using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
public class ImpactTest : MonoBehaviour
{
    IReactable unit = null;

    //ѕусть у нас есть пушка, котора€ всегда толкает врага (если враг поддерживает такое поведение) с определенным импульсом.
    //“огда код будет что то типо такого
   
    ImpactData impactData;

    Vector3 impulse = new Vector3(0, 0, 1);
    private void Awake()
    {
        impactData = new ImpactData(gameObject);
        impactData.AddImpact(new ImpactImpulse(impulse));

        impactData.AddImpact(new ImpactFireDamage(10, 20));
    }

    void Damage()
    {
        float damage = CalculateDamage();
        impactData.AddImpact(new ImpactDamage(damage));

        unit.Impact(impactData);
    }
    float CalculateDamage()
    {
        return 10;
    }
}
