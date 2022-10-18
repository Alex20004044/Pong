using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
using System;
using UniRx;

public class MVPTest : MonoBehaviour
{
    //IObserver<IDeltaRange<float>> deltaRangeObserver;
    DeltaRangeToText deltaRangeToText;
    IObservable<IDeltaRange<float>> deltaRangeSource;

    private void Awake()
    {
        //deltaRangeToText = GetComponent<DeltaRangeToText>();
        //deltaRangeSource = GetComponent<IObservable<IDeltaRange<float>>>();

        deltaRangeSource.Subscribe(deltaRangeToText).AddTo(this);
    }
}
