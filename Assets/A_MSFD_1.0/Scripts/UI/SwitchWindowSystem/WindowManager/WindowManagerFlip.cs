using MSFD.UI;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

namespace MSFD.UI
{
    /// <summary>
    /// This class use StopAllCoroutines on Window script for correctWork, so be carefull
    /// </summary>
    public class WindowManagerFlip : MonoBehaviour, IWindowManager
    {
        [Range(0f, AS.Constants.maxWindowOpenTime)]
        [SerializeField]
        float flipTime = 1;
        [SerializeField]
        float updateDelay = 0.02f;
        [SerializeField]
        Vector2 closeWindowDirection = new Vector2(-1, 0);

        void Awake()
        {
            closeWindowDirection.Normalize();
        }


        [Button]
        public void Close(Window _window)
        {
            //_window.StopCoroutine(nameof(CloseRoutine));
            //_window.StopCoroutine(nameof(OpenRoutine));
            _window.StopAllCoroutines();
            _window.StartCoroutine(CloseRoutine(GetCloseTargetPosition(), _window));
        }
        [Button]
        public void InstantClose(Window _window)
        {
            _window.StopAllCoroutines();
            _window.GetComponent<RectTransform>().anchoredPosition = -GetCloseTargetPosition();
            _window.DisableWindow();
        }
        [Button]
        public void Open(Window _window)
        {
            _window.StopAllCoroutines();
            _window.StartCoroutine(OpenRoutine(new Vector2(0, 0), _window));
        }
        Vector2 GetCloseTargetPosition()
        {
            return new Vector2(Screen.width * closeWindowDirection.x, Screen.height * closeWindowDirection.y);
        }

        IEnumerator OpenRoutine(Vector2 targetPosition, Window _window)
        {
            _window.EnableWindow();
            RectTransform windowTransform = _window.GetComponent<RectTransform>();
            Vector2 startPosition = windowTransform.anchoredPosition;
            float time = 0;
            while (time <= 1) // Vector2.Distance(position,windowTransform.anchoredPosition) > sqrTreshold)
            {
                time += updateDelay / flipTime;
                windowTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, time);
                yield return new WaitForSecondsRealtime(updateDelay);
            }
        }
        IEnumerator CloseRoutine(Vector2 targetPosition, Window _window)
        {
            RectTransform windowTransform = _window.GetComponent<RectTransform>();
            Vector2 startPosition = windowTransform.anchoredPosition;
            float time = 0;
            while (time <= 1) // Vector2.Distance(position,windowTransform.anchoredPosition) > sqrTreshold)
            {
                time += updateDelay / flipTime;
                windowTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, time);
                yield return new WaitForSecondsRealtime(updateDelay);
            }
            _window.DisableWindow();
            windowTransform.anchoredPosition = -targetPosition;
        }
    }
}