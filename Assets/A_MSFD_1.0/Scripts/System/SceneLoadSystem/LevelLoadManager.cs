using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MSFD
{
    public class LevelLoadManager : SingletoneBase<LevelLoadManager>
    {
        SceneActivationMode levelActivationMode = SceneActivationMode.activateOnLoadEnd;
        AsyncOperation levelLoadingOperation;

        string currentLoadingLevelName;
        bool isLevelLoadingNow = false;

        public const float sceneLoadingCheckDelay = 0.2f;

#if UNITY_EDITOR
        [ProgressBar(0, 1)] [ReadOnly] [ShowInInspector] [Obsolete]
        float sceneLoadingProgressBar = 0;
#endif
        protected override void AwakeInitialization()
        {
            isLevelLoadingNow = false;
        }

        [Button]
        public void AllowSceneActivation()
        {
            if (levelActivationMode == SceneActivationMode.waitForExternalActivation)
            {
                ActivateScene();
            }
            else
            {
                Debug.LogError("Attempt to allow scene activation in incorrect mode");
            }
        }

        [Button("Load Level")]
        public void LoadLevel(string levelName,
            SceneActivationMode sceneActivationMode = SceneActivationMode.activateOnLoadEnd)
        {
            if (isLevelLoadingNow)
            {
                Debug.LogError("Level "+ currentLoadingLevelName + " is loading now. Load "+ levelName +" level is cancelled");
                return;
            }
            try
            {
                isLevelLoadingNow = true;
                currentLoadingLevelName = levelName;

                levelActivationMode = sceneActivationMode;
                levelLoadingOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
                Messenger.Broadcast(SystemEvents.I_SCENE_LOADING_STARTED, MessengerMode.DONT_REQUIRE_LISTENER);

                levelLoadingOperation.completed += (asyncOperation) => { OnLevelLoaded(); };
                if (sceneActivationMode == SceneActivationMode.activateOnLoadEnd)
                {
                    ActivateScene();
                }
                else
                {
                    levelLoadingOperation.allowSceneActivation = false;
                }

                StartCoroutine(SceneLoadingCheck());
            }
            catch (Exception exeption)
            {
                Debug.LogError(exeption);
                isLevelLoadingNow = false;
            }
        }

        [Button("Restart Level")]
        public void RestartLevel(SceneActivationMode sceneActivationMode = SceneActivationMode.activateOnLoadEnd)
        {
            LoadLevel(SceneManager.GetActiveScene().name, sceneActivationMode);
            Messenger.Broadcast(SystemEvents.I_SCENE_RESTART, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        IEnumerator SceneLoadingCheck()
        {
            while (levelLoadingOperation.progress < 0.9)
            {
                float sceneLoadingProgress = (levelLoadingOperation.progress / 0.9f);
#if UNITY_EDITOR
                sceneLoadingProgressBar = sceneLoadingProgress;
#endif
                Messenger<float>.Broadcast(SystemEvents.I_float_SCENE_LOADING_PROGRESS, sceneLoadingProgress,
                    MessengerMode.DONT_REQUIRE_LISTENER);
                yield return new WaitForSecondsRealtime(sceneLoadingCheckDelay);
            }
        }
        [Button("Load Level by Index")]
        public void LoadLevel(int levelIndex,
            SceneActivationMode sceneActivationMode = SceneActivationMode.activateOnLoadEnd)
        {
            LoadLevel(SceneUtility.GetScenePathByBuildIndex(levelIndex), sceneActivationMode);
        }
        void ActivateScene()
        {
            levelLoadingOperation.allowSceneActivation = true;
        }

        void OnLevelLoaded()
        {
            isLevelLoadingNow = false;
#if UNITY_EDITOR
            sceneLoadingProgressBar = 0;
#endif
            Messenger.Broadcast(SystemEvents.I_SCENE_LOADING_COMPLETED, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        [Button]
        public void CloseApllication()
        {
            Debug.Log("CloseApplication");
            Messenger.Broadcast(SystemEvents.I_APPLICATION_QUIT, MessengerMode.DONT_REQUIRE_LISTENER);
            Application.Quit(0);
        }

        public enum SceneActivationMode
        {
            activateOnLoadEnd,
            waitForExternalActivation
        };
    }
}