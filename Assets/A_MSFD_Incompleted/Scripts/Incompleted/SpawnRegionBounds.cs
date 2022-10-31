using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MSFD.PhoenixSpawnSystem
{
    public class SpawnRegionBounds : SpawnRegionBase
    {
        [SerializeField]
        Vector3 boundsSize = new Vector3(1, 0, 1);

        public override Coordinate GetSpawnCoord()
        {
            Coordinate spawnCoord = new Coordinate();
            spawnCoord.position = AuxiliarySystem.RandomPointInBounds(new Bounds(transform.position, boundsSize));

            spawnCoord.rotation = DefineRotation();
            return spawnCoord;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = SpawnRegionBase.spawnRegionColor;
            //Gizmos.DrawCube(transform.position, boundsSize);
            Gizmos.DrawWireCube(transform.position, boundsSize);
        }
    }
}