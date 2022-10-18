using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField]
        float despawnTime = 0;
        [SerializeField]
        SpawnType spawnType;
        [SerializeField]
        DespawnType despawnType;
        public void Destroy()
        {
            PC.Despawn(gameObject, despawnTime, spawnType, despawnType);
        }
    }
}