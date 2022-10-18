
using UnityEngine;

namespace MSFD.UI
{
    /// <summary>
    /// This interface is needed to control process of appearing and disappearing Windows
    /// </summary>
    public interface IWindowManager
    {
        void Open(Window _window);
        void Close(Window _window);
        void InstantClose(Window _window);
    }
}