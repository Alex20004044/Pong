using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    public class SpawnRegionPoint : SpawnRegionBase
    {
        public override Coordinate GetSpawnCoord()
        {
            return new Coordinate(transform.position, DefineRotation());
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = spawnRegionSolidColor;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }
}
