using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static MSFD.IRechargable<float>;

namespace MSFD
{
    public class FloatObserverToSliderPro : FieldObserverToBase<float>
    {
        [SerializeField]
        SliderData increaseSlider;
        [SerializeField]
        SliderData mainSlider;
        [SerializeField]
        SliderData decreaseSlider;

        [SerializeField]
        float delayBeforeStart = 0.5f;
        [SerializeField]
        float transitionTime = 0.5f;
        [SerializeField]
        float qiuckTransitionTime = 0.1f;

        float currentValue = 0;

        CompositeDisposable disposables = new CompositeDisposable();

        protected override void Awake()
        {
            base.Awake();
            increaseSlider.Init();
            mainSlider.Init();
            decreaseSlider.Init();
        }

        public override void OnNext(float value)
        {
            disposables.Dispose();
            float delta = value - currentValue;
            if (delta >= 0)
            {
                //IncreaseProcess
                increaseSlider.StartTransition(value, 0, qiuckTransitionTime);
                increaseSlider.transitionCore.GetObsOnDestinationReached().Subscribe((x) => {
                    Debug.Log("ONREACHED");
                    mainSlider.StartTransition(value, delayBeforeStart, transitionTime);
                    decreaseSlider.StartTransition(value, delayBeforeStart, transitionTime);

                    }).AddTo(disposables);
            }
            else
            {
                //DecreaseProcess
                mainSlider.StartTransition(value, 0, qiuckTransitionTime);
                mainSlider.transitionCore.GetObsOnDestinationReached().Subscribe((x) => {
                    increaseSlider.StartTransition(value, delayBeforeStart, transitionTime);
                    decreaseSlider.StartTransition(value, delayBeforeStart, transitionTime);
                }).AddTo(disposables);

            }
            currentValue = value;
        }

        [System.Serializable]
        class SliderData
        {
            public InterfaceField<IObserver<float>> slider;
            public FloatObsTransitionCore transitionCore;

            public void Init()
            {
                transitionCore.Subscribe(slider.i);
            }

            public void StartTransition(float destinationValue, float delayBeforeStartTransition, float transitionTime)
            {
                Observable.Timer(System.TimeSpan.FromSeconds(delayBeforeStartTransition)).
                    Subscribe((x) => transitionCore.StartSpecialTransitionTime(destinationValue, transitionTime));
            }
        }
    }
}