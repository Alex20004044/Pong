using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IModifier<T>
{
    Func<T, T> GetModifier();
}
