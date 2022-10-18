using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.DebugTool
{
    public class DebugLog : MonoBehaviour
    {
        [SerializeField]
        string message;
        [SerializeField]
        DebugLogType logType;
        public void LogMessage()
        {
            LogMessage(message);
        }
        public void LogMessage(string message)
        {
            switch(logType)
            {
                case DebugLogType.debug:
                    {
                        Debug.Log(message);
                        break;
                    }
                case DebugLogType.warning:
                    {
                        Debug.LogWarning(message);
                        break;
                    }
                case DebugLogType.error:
                    {
                        Debug.LogError(message);
                        break;
                    }
            }
        }
        public void LogCurrentTimeMessage(string message)
        {
            LogMessage(message + " " + System.DateTime.Now.TimeOfDay);
        }
        enum DebugLogType {debug, warning, error};
    }
}
