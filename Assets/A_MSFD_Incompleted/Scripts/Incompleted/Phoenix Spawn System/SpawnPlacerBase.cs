using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    public abstract class SpawnPlacerBase : MonoBehaviour
    {
        [SerializeField]
        string[] targetUnitTags = null;
        public abstract void ActivateSpawn(List<UnitGroup> unitGroups, System.Action onSpawnAllUnits);
        public virtual string[] GetTargetUnitTags()
        {
            return targetUnitTags;
        }

    }
}