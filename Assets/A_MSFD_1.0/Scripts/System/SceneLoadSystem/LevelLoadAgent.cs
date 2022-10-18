using System.Collections;
using System.Collections.Generic;
using MSFD;
using UnityEngine;

public class LevelLoadAgent : MonoBehaviour
{
    [SerializeField]
    private LevelLoadManager.SceneActivationMode sceneActivationMode =
        LevelLoadManager.SceneActivationMode.activateOnLoadEnd;
    public void LoadLevel(string levelname)
    {
        LevelLoadManager.Instance.LoadLevel(levelname, sceneActivationMode);
    }
    public void LoadLevel(string levelname,
        LevelLoadManager.SceneActivationMode sceneActivationMode)
    {
        LevelLoadManager.Instance.LoadLevel(levelname, sceneActivationMode);
    }
    public void LoadLevel(int levelIndex)
    {
        LevelLoadManager.Instance.LoadLevel(levelIndex, sceneActivationMode);
    }
    public void LoadLevel(int levelIndex, LevelLoadManager.SceneActivationMode sceneActivationMode)
    {
        LevelLoadManager.Instance.LoadLevel(levelIndex, sceneActivationMode);
    }
    public void RestartLevel()
    {
        LevelLoadManager.Instance.RestartLevel();
    }

    public void AllowSceneActivation()
    {
        LevelLoadManager.Instance.AllowSceneActivation();
    }
    public void CloseApplication()
    {
        LevelLoadManager.Instance.CloseApllication();
    }
}