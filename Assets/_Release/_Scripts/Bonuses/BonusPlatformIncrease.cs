using MSFD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class BonusPlatformIncrease : BonusBase
    {
        [SerializeField]
        float increaseCoefficient = 1.5f;

        [SerializeField]
        uint targetBumpGoIndex = 0;

        IDisposable disposable;
        protected override void ActivateBonus()
        {
            GameObject[] bumpsStack = unit.GetComponent<IBumpGOGetter>().GetBumpGos();
            if (targetBumpGoIndex < bumpsStack.Length)
            {
                var go = bumpsStack[targetBumpGoIndex];
                if (go != null)
                {
                    disposable = go.GetComponent<ISizeChangable>().GetSizeModifiable().AddMod(SizeMod);
                    return;
                }
            }
            Debug.Log("Bonus is not activated due to unfilled bumb GOs array");
        }

        float SizeMod(float x)
        {
            x *= increaseCoefficient;
            return x;
        }

        protected override void DeactivateBonus()
        {
            disposable?.Dispose();
        }
    }
}