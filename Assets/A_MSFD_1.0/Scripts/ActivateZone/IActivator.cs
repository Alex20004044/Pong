using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public interface IActivator
    {
        bool TryActivate();
    }
}