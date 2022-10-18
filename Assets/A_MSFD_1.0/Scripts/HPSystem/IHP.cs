using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace MSFD
{
    public interface IHP
    {
        IDeltaRange<float> GetHP();
    }
}
