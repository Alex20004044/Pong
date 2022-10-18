using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
using UnityEngine.Events;
namespace MSFD
{
    public class InstantiatorRespawn : MonoBehaviour
    {
        [Header("Require IZoneObserver")]
        [SerializeField]
        GameObject prefab;

        [SerializeField]
        float minRespawnTime = 20f;
        [SerializeField]
        float maxRespawnTime = 30f;

        public UnityEvent onTargetInZone;
        public UnityEvent onStartWaitToSpawn;
        public UnityEvent onInstantiate;

        [SerializeField]
        InterfaceField<IZoneObserver> zoneObserver;

        bool isWaitForRespawn = false;

        private void Awake()
        {
            zoneObserver.i.onTriggerEnterTransform += OnTargetInZone;
        }
        private void OnDestroy()
        {
            zoneObserver.i.onTriggerEnterTransform -= OnTargetInZone;
        }
        private void Start()
        {
            Instantiate();
        }

        void OnTargetInZone(Transform target)
        {
            onTargetInZone.Invoke();
            if (!isWaitForRespawn)
            {
                isWaitForRespawn = true;
                onStartWaitToSpawn.Invoke();
                Invoke("Instantiate", Random.Range(minRespawnTime, maxRespawnTime));
            }
        }

        private void Instantiate()
        {
            isWaitForRespawn = false;
            GameObject go = PC.Spawn(prefab, transform.position, transform.rotation);
            go.transform.SetParent(transform);
            onInstantiate.Invoke();
        }
    }
}