using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public interface IVector2Settable
    {
        void SetDirection(Vector2 direction);
    }
}