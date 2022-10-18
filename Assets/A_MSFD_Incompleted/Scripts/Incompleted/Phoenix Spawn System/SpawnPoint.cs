using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    [System.Obsolete("Use SpawnRegionPoint instead")]
    public class SpawnPoint : SpawnRegionBase
    {
        public override Coordinate GetSpawnCoord()
        {
            return new Coordinate(transform.position, DefineRotation());
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = spawnRegionColor;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }
}
