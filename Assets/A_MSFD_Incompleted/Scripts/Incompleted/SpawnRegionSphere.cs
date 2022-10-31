using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    public class SpawnRegionSphere : SpawnRegionBase
    {
        [Sirenix.OdinInspector.MinValue(0)]
        [SerializeField]
        float radius = 5f;
        [Header("If true => objects will be spawned on zero height (Y == 0)")]
        [SerializeField]
        bool isDisableYAxis = true;
        public override Coordinate GetSpawnCoord()
        {
            Coordinate spawnCoord = new Coordinate();
            spawnCoord.position = AuxiliarySystem.RandomPointInSphere(transform.position, radius);
            if(isDisableYAxis)
            {
                spawnCoord.position.y = 0;
            }
            spawnCoord.rotation = DefineRotation();
            return spawnCoord;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = SpawnRegionBase.spawnRegionColor;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}