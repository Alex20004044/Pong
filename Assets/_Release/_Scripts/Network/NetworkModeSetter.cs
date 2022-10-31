using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pong
{
    public class NetworkModeSetter : MonoBehaviour
    {
        [SerializeField]
        TMP_Dropdown dropdown;

        private void Awake()
        {
            dropdown.onValueChanged.AddListener(OnChoosedMode);
        }

        private void OnDestroy()
        {
            dropdown.onValueChanged.RemoveListener(OnChoosedMode);
        }
        void OnChoosedMode(int mode)
        {
            IPManager.Instance.SetMultiplayerMode((NetworkEventsPong.MultiplayerMode)mode);
        }
    }
}