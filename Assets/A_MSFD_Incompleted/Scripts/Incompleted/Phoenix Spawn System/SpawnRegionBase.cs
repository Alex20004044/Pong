using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    public abstract class SpawnRegionBase : MonoBehaviour
    {
        [SerializeField]
        protected SpawnCoordRotationMode spawnCoordRotationMode = SpawnCoordRotationMode.random;
        public abstract Coordinate GetSpawnCoord();
        protected Quaternion DefineRotation()
        {
            if (spawnCoordRotationMode == SpawnCoordRotationMode.parent)
            {
                return transform.rotation;
            }
            else if(spawnCoordRotationMode == SpawnCoordRotationMode.random)
            {
                return AS.Rand.RandomRotationYAxis();
            }
            else 
            {
                return Quaternion.identity;
            }

        }
        public static Color spawnRegionColor = new Color(1f, 0.2f, 1f, 0.8f);
        public static Color spawnRegionSolidColor = new Color(1f, 0.2f, 1f, 1f);
    }
    
    public enum SpawnCoordRotationMode { identity, random, parent};
}