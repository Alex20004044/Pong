using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
namespace MSFD.UI
{
    public class LoadingScreenController : MonoBehaviour
    {
        [InfoBox(@"simpleTransition - just activate start transition animation on SCENE_LOADING_STARTED and activate end transition animation on SCENE_LOADING_COMPLETED
---
smoothTransition - activate end transition animation only when new scene was loaded and start animation was completed
---
activateSceneOnStartAnimationComplete - allow scene activation in application manager after start transition animation complete")]
        [SerializeField]
        ScreenControllerMode mode = ScreenControllerMode.smoothTransition;
        [SerializeField]
        Animator transitionAnimation;

        [SerializeField]
        float animationTime = 1f;

        bool isStartTransitionAnimationComplete = false;
        bool isNewSceneActivated = false;

        static string startLoadingBoolName = "StartLoading";
        static string endLoadingBoolName = "EndLoading";

        private void Awake()
        {
            Messenger.AddListener(SystemEvents.I_SCENE_LOADING_STARTED, OnSceneLoadingStarted);
            Messenger.AddListener(SystemEvents.I_SCENE_LOADING_COMPLETED, OnSceneLoadingCompleted);
            Messenger.AddListener(UIEvents.START_TRANSITION_COMPLETE, OnStartTransitionAnimationComplete);

            transitionAnimation.speed = transitionAnimation.speed / animationTime;
        }
        private void OnDestroy()
        {
            Messenger.RemoveListener(SystemEvents.I_SCENE_LOADING_STARTED, OnSceneLoadingStarted);
            Messenger.RemoveListener(SystemEvents.I_SCENE_LOADING_COMPLETED, OnSceneLoadingCompleted);
            Messenger.RemoveListener(UIEvents.START_TRANSITION_COMPLETE, OnStartTransitionAnimationComplete);
        }

        void OnSceneLoadingStarted()
        {
            //Reset all variables
            isNewSceneActivated = false;
          
            //Attention!!! Для корректной работы при первом включении на загрузочной сцене
            if (transitionAnimation.GetBool(startLoadingBoolName) == transitionAnimation.GetBool(endLoadingBoolName))
            {
                isStartTransitionAnimationComplete = true;
                return;
            }
            else
            {
                isStartTransitionAnimationComplete = false;
                ActivateLoadingScreenStartedAnimation();
            }
        }

        void OnSceneLoadingCompleted()
        {
            isNewSceneActivated = true;
            switch (mode)
            {
                case ScreenControllerMode.simpleTransition:
                    {
                        ActivateLoadingScreenCompletedAnimation();
                        break;
                    }
                case ScreenControllerMode.smoothTransition:
                    {
                        TrySmoothTransition();
                        break;
                    }
                case ScreenControllerMode.activateSceneOnStartAnimationComplete:
                    {
                        ActivateLoadingScreenCompletedAnimation();
                        break;
                    }
            }
        }
        void OnStartTransitionAnimationComplete()
        {
            isStartTransitionAnimationComplete = true;
            switch (mode)
            {
                case ScreenControllerMode.smoothTransition:
                    {
                        TrySmoothTransition();
                        break;
                    }
                case ScreenControllerMode.activateSceneOnStartAnimationComplete:
                    {
                        //!!!!!!!!!!!!!!!!!!!
                        //ApplicationManager.Instance.AllowSceneActivation();
                        break;
                    }
            }
        }
        void TrySmoothTransition()
        {
            if(isNewSceneActivated && isStartTransitionAnimationComplete)
            {
                ActivateLoadingScreenCompletedAnimation();
            }
        }
        void ActivateLoadingScreenStartedAnimation()
        {
            transitionAnimation.SetBool("StartLoading", true);
            transitionAnimation.SetBool("EndLoading", false);
        }

        void ActivateLoadingScreenCompletedAnimation()
        {
            transitionAnimation.SetBool("StartLoading", false);
            transitionAnimation.SetBool("EndLoading", true);
        }
        /// <summary>
        /// simpleTransition - just activate start transition animation on SCENE_LOADING_STARTED and activate end transition animation on SCENE_LOADING_COMPLETED
        /// smoothTransition - activate end transition animation only when new scene was loaded and start animation was completed
        /// activateSceneOnStartAnimationComplete - allow scene activation in application manager after start transition animation complete
        /// </summary>
        enum ScreenControllerMode { simpleTransition, smoothTransition, activateSceneOnStartAnimationComplete }
    }
}
