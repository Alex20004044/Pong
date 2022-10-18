using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MSFD.SpawnSystem
{
    public class SpawnHub : MonoBehaviour
    {
        [SerializeField]
        SpawnHubData spawnHubData;
        [SerializeField]
        UnityEvent onEndSpawn = new UnityEvent();
        //Spawn placers for every spawn list in spawnHubData
        [SerializeField]
        List<SpawnPlacerBase> spawnPlacers = new List<SpawnPlacerBase>();

        List<SpawnerBase> spawnerBases = new List<SpawnerBase>();
        SpawnerState spawnerState = SpawnerState.disable;
        int waveInd = 0;

        int endWaveSpawnersCount;
        int parallelAndConsistentSpawnersCount;
        int endWaveParallelAndConsistentSpawnersCount;
        Queue<SpawnerBase> consistentSpawners;


        GameObject spawnersParent;
        public void StartSpawn()
        {
            if (spawnerState != SpawnerState.disable)
            {
                Debug.LogError("Attempt to activate activated spawner!");
                return;
            }
            Initializate();
            spawnerState = SpawnerState.waitBetweenWaves;
            SpawnWave(waveInd);
        }

        void Initializate()
        {
            if(spawnersParent != null)
            {
                Destroy(spawnersParent);
            }

            waveInd = 0;

            spawnersParent = new GameObject("SpawnersParent");
            spawnersParent.transform.SetParent(transform);
            spawnersParent.transform.localPosition = Vector3.zero;
            for(int i = 0; i < spawnHubData.spawnLists.Count; i++)
            {
                GameObject spawnerGO = Instantiate<GameObject>(spawnHubData.spawnLists[i].GetSpawnerPrefab());
                spawnerGO.transform.SetParent(spawnersParent.transform);
                spawnerGO.transform.localPosition = Vector3.zero;
                spawnerBases.Add(spawnerGO.GetComponent<SpawnerBase>());
                spawnerBases[i].Initializate(spawnHubData.spawnLists[i], spawnPlacers[i]);
            }
        }

        void SpawnWave(int waveInd)
        {
            if (spawnerState == SpawnerState.waitBetweenWaves)
            {
                spawnerState = SpawnerState.spawn;
                endWaveSpawnersCount = 0;
                parallelAndConsistentSpawnersCount = 0;
                endWaveParallelAndConsistentSpawnersCount = 0;
                consistentSpawners = new Queue<SpawnerBase>();
                SpawnerBase spawner;

                for (int i = 0; i < spawnHubData.spawnLists.Count; i++)
                {
                    spawner = spawnerBases[i];
                    if (spawner.SpawnList.workMode == WorkModeSpawnList.consistent || spawner.SpawnList.workMode == WorkModeSpawnList.parallel)
                    {
                        parallelAndConsistentSpawnersCount++;
                    }

                    if (spawnHubData.spawnLists[i].workMode == WorkModeSpawnList.individual || spawnHubData.spawnLists[i].workMode == WorkModeSpawnList.parallel)
                    {
                        spawner.SpawnWave(waveInd, OnWaveSpawnEndCallback);
                    }
                    else
                    {
                        consistentSpawners.Enqueue(spawner);
                    }
                }
                //Start spawn consistent groups
                if (consistentSpawners.Count > 0)
                {
                    consistentSpawners.Dequeue().SpawnWave(waveInd, OnWaveSpawnEndCallback);
                }
            }
            else
            {
                Debug.LogError("Attempt to EndWaveSpawner in incorrect SpawnerState");
            }
        }

        void OnWaveSpawnEndCallback(SpawnerBase spawner)
        {
            endWaveSpawnersCount++;
            if (spawner.SpawnList.workMode == WorkModeSpawnList.consistent)
            {
                endWaveParallelAndConsistentSpawnersCount++;
                //Spawn next member of consistent group
                if (consistentSpawners.Count > 0)
                {
                    consistentSpawners.Dequeue().SpawnWave(waveInd, OnWaveSpawnEndCallback);
                }
            }
            else if(spawner.SpawnList.workMode == WorkModeSpawnList.parallel)
            {
                endWaveParallelAndConsistentSpawnersCount++;
            }

            switch (spawnHubData.GetNextWaveCondition(waveInd))
            {
                case SpawnHubData.SHDNextWaveCondition.externalActivation:
                    {
                        //do nothing
                        break;
                    }
                case SpawnHubData.SHDNextWaveCondition.parallelsOrConsistent:
                    {
                        if(endWaveParallelAndConsistentSpawnersCount >= parallelAndConsistentSpawnersCount)
                        {
#if UNITY_EDITOR
                            if (endWaveParallelAndConsistentSpawnersCount > parallelAndConsistentSpawnersCount)
                            {
                                Debug.LogError("Error in spawners callback");
                            }
#endif
                            WaveIsSpawned();
                        }
                        break;
                    }
                case SpawnHubData.SHDNextWaveCondition.allSpawners:
                    {
                        if (endWaveSpawnersCount >= spawnHubData.spawnLists.Count)
                        {
#if UNITY_EDITOR
                            if(endWaveSpawnersCount > spawnHubData.spawnLists.Count)
                            {
                                Debug.LogError("Error in spawners callback");
                            }
#endif
                            WaveIsSpawned();
                        }
                        break;
                    }
            }
        }
        public void ExternalEventWaveIsSpawned()
        {
            if(spawnHubData.GetNextWaveCondition(waveInd) == SpawnHubData.SHDNextWaveCondition.externalActivation)
            {
                WaveIsSpawned();
            }
            else
            {
                Debug.LogError("Attempt to do external activation in " + spawnHubData.GetNextWaveCondition(waveInd) + " mode" );
            }
        }
        void WaveIsSpawned()
        {
            if (spawnerState == SpawnerState.spawn)
            {
                spawnerState = SpawnerState.waitBetweenWaves;
                //Event to every spawner that wave is spawned. They should cancel their spawn if they want
                for (int i = 0; i < spawnHubData.spawnLists.Count; i++)
                {
                    spawnerBases[i].OnWaveIsSpawned();
                }
                StartCoroutine(WaitBetweenWaves());
            }
            else
            {
                Debug.LogError("Attempt to EndWaveSpawner in incorrect SpawnerState");
            }

        }
        IEnumerator WaitBetweenWaves()
        {
            yield return new WaitForSeconds(spawnHubData.GetDelayBeforeNextWave(waveInd));
            SpawnNextWave();
        }
        void SpawnNextWave()
        {
            if (spawnerState == SpawnerState.waitBetweenWaves)
            {
                waveInd++;
                if (waveInd < spawnHubData.WavesCount)
                {
                    SpawnWave(waveInd);
                }
                else
                {
                    EndSpawn();
                }
            }
            else
            {
                Debug.LogError("Attempt to spawn next wave in incorrect SpawnerState");
            }
        }
        void EndSpawn()
        {
            spawnerState = SpawnerState.endSpawn;
            onEndSpawn.Invoke();
        }
        public enum SpawnerState { disable, spawn, waitBetweenWaves, endSpawn }
    }
}