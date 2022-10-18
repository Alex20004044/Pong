using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public interface IData: IObservable<IData>
    {
        string Name { get; set; }
        string GetPath();
        string GetTextView();


    }

/*    public interface IDataNodeGeneric<T>:IData, IReactiveProperty<T>
    {
        new T Value { get; set; }
    }*/

}
