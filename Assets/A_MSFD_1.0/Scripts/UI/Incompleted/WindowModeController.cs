    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowModeController : MonoBehaviour
{

    
    public int defaultMode = 0;
    public GameObject[] windowModesGo;
    
    [Header("If you want to use this property you should add this script to main window for window modes or add SetDefaultWindowMode() to back Button")]
    public bool isNeedResetToDefaultModeOnDisable = true;

    int currentModeInd;

    private void Awake()
    {
        currentModeInd = defaultMode;
        foreach(GameObject x in windowModesGo)
        {
            x.SetActive(false);
        }
        SetDefaultWindowMode();
    }

    private void OnDisable()
    {
        if(isNeedResetToDefaultModeOnDisable)
        {
            SetDefaultWindowMode();
        }
    }
    public void ChangeCurrentMode(int index)
    {
        windowModesGo[currentModeInd].SetActive(false);
        
        windowModesGo[index].SetActive(true);
        currentModeInd = index;
    }

    public void SetDefaultWindowMode()
    {
        ChangeCurrentMode(defaultMode);
    }
}

