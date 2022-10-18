using MSFD;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class LoadingScreenControllerDefault : MonoBehaviour
{
    public UnityEvent onSceneLoadingStarted;
    public UnityEvent onSceneLoadingCompleted;
    private void Awake()
    {
        Messenger.AddListener(SystemEvents.I_SCENE_LOADING_STARTED, OnSceneLoadingStarted);
        Messenger.AddListener(SystemEvents.I_SCENE_LOADING_COMPLETED, OnSceneLoadingCompleted);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(SystemEvents.I_SCENE_LOADING_STARTED, OnSceneLoadingStarted);
        Messenger.RemoveListener(SystemEvents.I_SCENE_LOADING_COMPLETED, OnSceneLoadingCompleted);
    }
    [Button]
    void OnSceneLoadingStarted()
    {
        onSceneLoadingStarted.Invoke();
    }
    [Button]
    void OnSceneLoadingCompleted()
    {
        onSceneLoadingCompleted.Invoke();
    }
}
