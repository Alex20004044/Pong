using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
using System;

public class TestDelayManager : SingletoneBase<TestDelayManager>, IManager
{
    [SerializeField]
    float initializationTime = 5f;
    protected override void AwakeInitialization()
    {
       
    }

    void IManager.ManagerInitialization(Action OnInitializedCallback)
    {
        StartCoroutine(InitializationDelay(OnInitializedCallback));
    }
    IEnumerator InitializationDelay(Action OnInitializedCallback)
    {
        yield return new WaitForSeconds(initializationTime);
        OnInitializedCallback.Invoke();
    }
}
