using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutCanvasGroup : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;
    [SerializeField]
    StartAction startAction = StartAction.fadeOut;
    
    [SerializeField]
    float fadeInTime = 1f;
    [SerializeField]
    float fadeOutTime = 1f;
    [SerializeField]
    float updateDelay = 0.1f;

    private void Awake()
    {
        if(startAction == StartAction.fadeIn)
        {
            canvasGroup.alpha = 0;
            FadeIn();
        }
        else if(startAction == StartAction.fadeOut)
        {
            canvasGroup.alpha = 1;
            FadeOut();
        }
    }

    [Button]
    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInRoutine());
    }
    [Button]
    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutRoutine());
    }
    IEnumerator FadeInRoutine()
    {
        float delta = (1 / fadeInTime) * updateDelay;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += delta;
            yield return new WaitForSecondsRealtime(updateDelay);
        }
    }
    IEnumerator FadeOutRoutine()
    {
        float delta = (1 / fadeOutTime) * updateDelay;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= delta;
            yield return new WaitForSecondsRealtime(updateDelay);
        }
    }
    public enum StartAction { none, fadeIn, fadeOut}
}
