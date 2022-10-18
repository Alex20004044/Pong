using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using TimeMode = MSFD.IRechargable<float>.TimeMode;
namespace MSFD
{
    [System.Serializable]
    public class FloatObsTransitionCore : IObservable<float>, IObserver<float>
    {
        [Tooltip(@"Curve defines how currentValue will be move
                                   time => transition time
                                   value => currentValue = sourceValue + delta * value
delta = destinationValue - sourceValue")]
        [OnValueChanged("@" + nameof(UpdateAnimationTime) + "()")]
        [SerializeField]
        AnimationCurve transitionCurve = new AnimationCurve(new Keyframe(0,0), new Keyframe(1,1));

        [HorizontalGroup("TimeSettings", LabelWidth = 120)]
        [SerializeField]
        TimeMode timeMode = TimeMode.scaledTime;
        [HorizontalGroup("TimeSettings", LabelWidth = 120)]
        [SerializeField]
        float updateDelay = 0.1f;

        [HorizontalGroup("TransitionSettings")]
        [OnValueChanged("@" + nameof(UpdateAnimationTime) + "()")]
        [SerializeField]
        float animationSpeed = 1;
#if UNITY_EDITOR
        [HorizontalGroup("TransitionSettings")]
        [ShowInInspector]
        [Obsolete(EditorConstants.editorOnly)]
        float AnimationTime
        {
            get
            {
                if (transitionCurve != null && transitionCurve.length > 0)
                    return transitionCurve[transitionCurve.length - 1].time / animationSpeed;
                else
                    return 0;
            }
            set
            {
                animationSpeed = transitionCurve[transitionCurve.length - 1].time / value;
            }
        }
#endif
        [SerializeField]
        bool isInstantSetFirstValue = false;

        float destinationValue = 0;
        float SourceValue { get => sourceValue; set { sourceValue = value; currentValue.Value = value; onDestinationReached.OnNext(destinationValue); } }
        float sourceValue = 0;
        ReactiveProperty<float> currentValue = new ReactiveProperty<float>();

        IDisposable disposable;

        bool isFirstValueSetted = false;

        Subject<float> onDestinationReached = new Subject<float>();

        #region Base

        ~FloatObsTransitionCore()
        {
            disposable?.Dispose();
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return currentValue.Subscribe(observer);
        }
        public void OnCompleted()
        {
            disposable?.Dispose();
        }

        public void OnError(Exception error)
        {
            Debug.LogError(error);
        }

        public void OnNext(float value)
        {
            if (isFirstValueSetted || !isInstantSetFirstValue)
                StartSpecialTransition(value, animationSpeed, updateDelay);
            else
            {
                isFirstValueSetted = true;
                DirectSet(value);
            }
        }
        #endregion
        public void DirectSet(float value)
        {
            SourceValue = value;
        }
        public void StartSpecialTransitionTime(float value, float animationTime = 1, float updateDelay = 0.1f)
        {
            StartSpecialTransition(value, transitionCurve[transitionCurve.length - 1].time / animationTime, updateDelay);
        }
        public void StartSpecialTransition(float value, float animationSpeed = 1, float updateDelay = 0.1f)
        {
            disposable?.Dispose();
            sourceValue = currentValue.Value;

            destinationValue = value;
            float _updateDelay = updateDelay;
            float _animationSpeed = animationSpeed;
            int _stepsCount = Mathf.CeilToInt((transitionCurve[transitionCurve.length - 1].time / _updateDelay) / _animationSpeed);
            float _delta = destinationValue - SourceValue;
            float _time = 0;

            disposable = Observable.Interval(TimeSpan.FromSeconds(updateDelay)).Subscribe((x) =>
                DisplayRoutine());

            void DisplayRoutine()
            {
                if (_time <= transitionCurve[transitionCurve.length - 1].time)
                {
                    currentValue.Value = SourceValue + _delta * transitionCurve.Evaluate(_time);
                    if (timeMode == TimeMode.realTime)
                        _time += updateDelay * _animationSpeed;
                    else
                        _time += updateDelay * _animationSpeed * Time.timeScale;
                }
                else
                {
                    SourceValue = destinationValue;

                    //Debug.Log("reached " + sourceValue);
                    disposable?.Dispose();
                    onDestinationReached.OnNext(destinationValue);
                }
            }
        }
        public IObservable<float> GetObsOnDestinationReached()
        {
            return onDestinationReached;
        }
        public void SetTimeMode(IRechargable<float>.TimeMode timeMode = IRechargable<float>.TimeMode.scaledTime)
        {
            this.timeMode = timeMode;
        }
        public void SetTransitionCurve(AnimationCurve transitionCurve)
        {
            this.transitionCurve = transitionCurve;
        }
#if UNITY_EDITOR
        [Obsolete(EditorConstants.editorOnly)]
        void UpdateAnimationTime()
        {
            AnimationTime = AnimationTime;
        }
#endif
    }
}