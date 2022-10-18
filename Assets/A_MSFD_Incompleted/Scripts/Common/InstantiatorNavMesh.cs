/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
namespace MSFD
{
    public class InstantiatorNavMesh : MonoBehaviour
    {
        [SerializeField]
        NavMeshSurface navMeshSurface;

        [SerializeField]
        GameObject spawnPrefab;

        [Button("Spawn")]
        public void Spawn()
        {
            GameObject so = PC.Spawn(spawnPrefab, AuxiliarySystem.RandomPointInBounds(navMeshSurface.navMeshData.sourceBounds), Quaternion.identity);
            NavMeshAgent navMeshAgent = so.AddComponent<NavMeshAgent>();
            Destroy(navMeshAgent, 0.1f);
        }
        [Button("Spawn Prefab")]
        public void Spawn(GameObject spawnPrefab)
        {
            GameObject so = PC.Spawn(spawnPrefab, AuxiliarySystem.RandomPointInBounds(navMeshSurface.navMeshData.sourceBounds), Quaternion.identity);
            NavMeshAgent navMeshAgent = so.AddComponent<NavMeshAgent>();
            Destroy(navMeshAgent, 0.1f);
        }
    }
}*/