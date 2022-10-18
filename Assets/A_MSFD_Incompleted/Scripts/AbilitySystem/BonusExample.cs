using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
public class BonusExample : BonusBase
{
    protected override void ActivateBonus()
    {
        InvokeRepeating("RepeatPosition", 0, 2);
    }

    protected override void DeactivateBonus()
    {
        CancelInvoke();
        unit.transform.position = Vector3.zero;
    }
    public void RepeatPosition()
    {
        unit.transform.position = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
    }
}
