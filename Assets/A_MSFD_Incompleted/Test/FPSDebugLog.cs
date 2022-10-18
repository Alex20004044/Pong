using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDebugLog : MonoBehaviour
{
    void Update()
    {
        Debug.Log("Time.renderedFrameCount " + Time.renderedFrameCount);
        Debug.Log("Time.frameCount " + Time.frameCount);
        Debug.Log("Time.deltaTime " + Time.deltaTime);
        Debug.Log("Time.smoothDeltaTime " + Time.smoothDeltaTime);
    }
}
