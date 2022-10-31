/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MSFD.PhoenixSpawnSystem
{
    public class SpawnRegionNavMesh : SpawnRegionBase
    {
        [SerializeField]
        NavMeshSurface navMeshSurface;
        public override Coordinate GetSpawnCoord()
        {
            Coordinate spawnCoord = new Coordinate();
            spawnCoord.position = AuxiliarySystem.RandomPointInBounds(navMeshSurface.navMeshData.sourceBounds);

            if(spawnCoordRotationMode == SpawnCoordRotationMode.identity)
            {
                spawnCoord.rotation = Quaternion.identity;
            }
            else
            {
                spawnCoord.rotation = AuxiliarySystem.GetRandomRotationYAxis();
            }

            return spawnCoord;
        }
    }
}*/