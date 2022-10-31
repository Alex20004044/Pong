using MSFD;
using MSFD.AS;
using MSFD.PhoenixSpawnSystem;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class BonusSpawner : NetworkBehaviour
    {
        [SerializeField]
        Vector2 spawnTime = new Vector2(10, 30);


        [SerializeField]
        NetworkObject[] bonusPrefabs;

        [SerializeField]
        SpawnRegionBase spawnRegion;
        
        private void Awake()
        {
            Messenger.Subscribe(GameEvents.I_GAME_STARTED, Activate).AddTo(this);
            Messenger.Subscribe(GameEvents.I_GAME_ENDED, Deactivate).AddTo(this);
        }
        public override void OnNetworkSpawn()
        {
            foreach(var x in bonusPrefabs)
            {
                NetworkManager.Singleton.AddNetworkPrefab(x.gameObject);
            }
        }
        public void Activate()
        {        
            StartCoroutine(SpawnRoutine());
        }
        public void Deactivate()
        {
            StopCoroutine(SpawnRoutine());
        }

        IEnumerator SpawnRoutine()
        {
            while(true)
            {
                yield return new WaitForSeconds(spawnTime.RandomPointInRange());
                Spawn();
            }
        }
        void Spawn()
        {
            int bonusIndex = Rand.GetRandomIndex(bonusPrefabs.Length);
            Vector3 spawnPosition = spawnRegion.GetSpawnCoord().position;
            StartCoroutine(WaitAndSpawn(0, bonusIndex, spawnPosition));
            //SpawnClientRPC(0, bonusIndex, spawnPosition);
        }
/*        [ClientRpc]
        void SpawnClientRPC(float time, int bonusIndex, Vector3 position)
        {
            if (IsHost) return;
            StartCoroutine(WaitAndSpawn(time, bonusIndex, position));
        }*/

        IEnumerator WaitAndSpawn(float time, int bonusIndex, Vector3 position)
        {
            if (time > 0)
                yield return new WaitForSeconds(time);

            GameObject bonus = Instantiate(bonusPrefabs[bonusIndex].gameObject);
            bonus.transform.position = position;
            NetworkObject bonusNetwork = bonus.GetComponent<NetworkObject>();
            bonusNetwork.Spawn(true);
        }
    }
}