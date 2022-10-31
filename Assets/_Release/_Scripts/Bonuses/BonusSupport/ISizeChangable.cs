using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public interface ISizeChangable
    {
        IModifiable<float> GetSizeModifiable();

    }
}