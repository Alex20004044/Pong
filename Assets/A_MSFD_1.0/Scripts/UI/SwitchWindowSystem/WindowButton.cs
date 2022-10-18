using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MSFD.UI
{
    [RequireComponent(typeof(Button))]
    public class WindowButton : MonoBehaviour
    {
        [Header("Enter which window open. Empty = \"BackButton\"")]
        [SerializeField]
        string windowName = string.Empty;

        Button button;
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(()=>OpenWindow());
        }
        void OpenWindow()
        {
            Messenger<string>.Broadcast(UIEvents.R_string_OPEN_WINDOW, windowName);
        }
    }
}