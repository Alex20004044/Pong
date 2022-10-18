using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace MSFD
{
    public class Instantiator : MonoBehaviour
    {
        [SerializeField]
        GameObject goPrefab;
        [SerializeField]
        SpawnType spawnType;
        [SerializeField]
        bool isShouldDestroyGo = false;
        [ShowIf("isShouldDestroyGo")]
        [Header("If destruct time less then 0 then will be immediate destruction")]
        [SerializeField]
        float destructGOTime = -1f;
        [ShowIf("isShouldDestroyGo")]
        [SerializeField]
        DespawnType despawnType;

        public void Instantiate()
        {
            GameObject go = PC.Spawn(goPrefab, spawnType, transform.position, transform.rotation, true);
            if (isShouldDestroyGo)
            {
                PC.Despawn(go, destructGOTime, spawnType, despawnType);
            }
            /*GameObject go = Instantiate<GameObject>(goPrefab);
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;

            if (destructPrefabTime != 0)
            {
                Destroy(go, destructPrefabTime);
            }*/
        }
    }
}