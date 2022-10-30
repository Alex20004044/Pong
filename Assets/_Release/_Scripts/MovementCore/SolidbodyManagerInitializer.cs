using Pong;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidbodyManagerInitializer : MonoBehaviour
{
    private void Start()
    {
        SolidBody2DManager.Instance.InitializeMapBounds(FindObjectOfType<SceneController>().GetSceneBounds());
    }
}
