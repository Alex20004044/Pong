using UnityEngine;
using UnityEngine.Events;
namespace MSFD.PhoenixSpawnSystem
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        SpawnList spawnList;

        [SerializeField]
        SpawnPlacerBase[] spawnPlacers;
        /*
        [SerializeField]
        UnityEvent onStartSpawn;
        [SerializeField]
        UnityEvent onEndSpawn;
        [SerializeField]
        UnityEvent onWaveStart;*/

        [SerializeField]
        SpawnerState spawnerState;
        [Sirenix.OdinInspector.ReadOnly]
        int waveInd = 0;
        [SerializeField]
        UnityEvent onNextWaveStartSpawn;
        [SerializeField]
        UnityEvent onEndSpawn;

        bool isWasExternalEventToSpawnNextWave = false;

        [Sirenix.OdinInspector.Button]
        public void StartSpawn()
        {
            waveInd = 0;
            spawnerState = new DisableState(this);
            spawnerState.SpawnNextWave();
        }
        public void SpawnNextWave()
        {
            spawnerState.SpawnNextWave();
        }
        [Sirenix.OdinInspector.Button]
        public void ExternalEventToSpawnWave()
        {
            isWasExternalEventToSpawnNextWave = true;
            SpawnNextWave();
        }

        void OnNextWaveStartSpawn()
        {
            onNextWaveStartSpawn.Invoke();
        }
        void OnEndSpawn()
        {
            onEndSpawn.Invoke();
        }
        public int GetWavesCount()
        {
            return spawnList.waves.Count;
        }
        [System.Serializable]
        public abstract class SpawnerState
        {
            protected Spawner spawner;
            protected SpawnerState(Spawner _spawner)
            {
                spawner = _spawner;
                spawner.spawnerState = this;
                Debug.Log("State: " + this.ToString());
            }
            public virtual void SpawnNextWave()
            {
                Debug.Log("Attempt to spawn next wave in " + this.ToString());
            }
        }

        public class DisableState : SpawnerState
        {
            public DisableState(Spawner _spawner) : base(_spawner)
            {
            }
            public override void SpawnNextWave()
            {
                Debug.Log("Spawn is started");
                new ActivateSpawnPlacers(spawner);
            }
        }
        public class ActivateSpawnPlacers : SpawnerState
        {
            public ActivateSpawnPlacers(Spawner _spawner) : base(_spawner)
            {
                spawner.OnNextWaveStartSpawn();
                SpawnWaveState spawnWaveState = new SpawnWaveState(spawner);
                foreach (SpawnPlacerBase x in spawner.spawnPlacers)
                {
                    x.ActivateSpawn(SpawnListProcessor.GetUnitsGroups(spawner.spawnList, spawner.waveInd, x.GetTargetUnitTags()), spawnWaveState.OnSpawnerEndSpawn);
                }
            }
        }
        public class SpawnWaveState : SpawnerState
        {
            int spawnPlacersCount;
            int readySpawnPlacersCount;
            bool isWaveConditionTime;
            bool isAllSpawnersReady;

            bool isActivateNextWaveState;
            public SpawnWaveState(Spawner _spawner) : base(_spawner)
            {
                spawnPlacersCount = spawner.spawnPlacers.Length;
                readySpawnPlacersCount = 0;
                isWaveConditionTime = false;
                isAllSpawnersReady = false;
                isActivateNextWaveState = false;
                spawner.StartCoroutine(ActivateWaiting());
            }

            public void OnSpawnerEndSpawn()
            {
                if (spawner.spawnerState == this)
                {
                    readySpawnPlacersCount++;
                    if (spawnPlacersCount == readySpawnPlacersCount)
                    {
                        isAllSpawnersReady = true;
                        SpawnNextWave();
                    }
                }
            }
            public override void SpawnNextWave()
            {
                if (spawner.spawnerState == this && CheckIfCanSpawnNextwave() && !isActivateNextWaveState)
                {
                    spawner.waveInd++;
                    Messenger<int>.Broadcast(GameEvents.SPAWN_WAVE, spawner.waveInd, MessengerMode.DONT_REQUIRE_LISTENER);
                    if (spawner.waveInd < spawner.spawnList.waves.Count)
                    {
                        spawner.StartCoroutine(ActivateWaveSpawn());
                    }
                    else
                    {
                        new EndSpawnState(spawner);
                    }
                }
            }
            System.Collections.IEnumerator ActivateWaiting()
            {
                yield return new WaitForSeconds(spawner.spawnList.waves[spawner.waveInd].wavesConditionTime);
                isWaveConditionTime = true;
                SpawnNextWave();
            }
            System.Collections.IEnumerator ActivateWaveSpawn()
            {
                isActivateNextWaveState = true;
                yield return new WaitForSeconds(spawner.spawnList.GetWaveDelay(spawner.waveInd - 1));
                spawner.isWasExternalEventToSpawnNextWave = false;
                new ActivateSpawnPlacers(spawner);
            }
            bool CheckIfCanSpawnNextwave()
            {
                Wave wave = spawner.spawnList.waves[spawner.waveInd];
                switch (wave.nextWaveCondition)
                {
                    case SpawnList.NextWaveCondition.allSpawnersReady:
                        {
                            return isAllSpawnersReady;
                        }
                    case SpawnList.NextWaveCondition.waitTimeOrAllSpawnersReady:
                        {
                            return isWaveConditionTime || isAllSpawnersReady;
                        }
                    case SpawnList.NextWaveCondition.timeWait:
                        {
                            return isWaveConditionTime;
                        }
                    case SpawnList.NextWaveCondition.externalEvent:
                        {
                            return spawner.isWasExternalEventToSpawnNextWave;
                        }
                    default:
                        {
                            Debug.LogError("Unknown next wave condition: " + wave.nextWaveCondition);
                            return false;
                        }
                }
            }
        }
        public class EndSpawnState : SpawnerState
        {
            public EndSpawnState(Spawner _spawner) : base(_spawner)
            {
                spawner.OnEndSpawn();
            }

            public override void SpawnNextWave()
            {
                Debug.LogError("Attempt to spawn next wave on EndState. You need to call StartSpawn to restart spawner");
            }
        }
    }
}
