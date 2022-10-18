using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = null;
        PC.Spawn(prefab, SpawnType.network);
        PC.Despawn(prefab, 2, SpawnType.network, DespawnType.pool);
        PC.CleanQueue(prefab.name);
        PC.RequestPreSpawn(prefab, 10, SpawnType.local, PreSpawnAmountMode.addAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
