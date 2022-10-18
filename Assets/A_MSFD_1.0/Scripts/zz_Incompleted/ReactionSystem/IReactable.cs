using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public interface IReactable
    {
        /// <summary>
        /// Вызываетя для нанесения урона, активации каких либо эффектов на цеелвом объекте. 
        /// Если объет способен поддерживать такое поведение, то он исполнит его специфичным для себя образом
        /// </summary>
        /// <param name="sender"></param>
        void Impact(ImpactData impactData);

        void AddListener(string impactType, Action<ImpactData> callback);
        void RemoveListener(string impactType, Action<ImpactData> callback);
    }
}