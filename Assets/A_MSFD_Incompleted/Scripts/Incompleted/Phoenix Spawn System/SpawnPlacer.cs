using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    public class SpawnPlacer : SpawnPlacerBase
    {
        [SerializeField]
        EndWaveCondition endWaveCondition = EndWaveCondition.whenAllUnitsDied;

        [SerializeField]
        bool isAutoSetPoints = true;
        //!
        [Sirenix.OdinInspector.HideIf("isAutoSetPoints")]
        [SerializeField]
        List<SpawnRegionBase> spawnRegions;

        bool isAllUnitsSpawned = false;
        Action onEndSpawn;

        private void Awake()
        {
            if (isAutoSetPoints)
            {
                spawnRegions.Clear();
                foreach (Transform s in transform)
                {
                    spawnRegions.Add(s.GetComponent<SpawnRegionBase>());
                }
            }
        }

        /*private void Start()
        {
            EnemyManager.instance.onNumEnemiesChanged.AddListener(OnNumEnemiesChanged);
        }
        private void OnDestroy()
        {
            EnemyManager.instance.onNumEnemiesChanged.RemoveListener(OnNumEnemiesChanged);
        }*/
        void OnNumEnemiesChanged()
        {
            /*if(EnemyManager.instance.NumLiveUnits == 0 && isAllUnitsSpawned)
            {
                onEndSpawn.Invoke();
            }*/
        }
        public override void ActivateSpawn(List<UnitGroup> unitGroups, Action onEndSpawn)
        {
            this.onEndSpawn = onEndSpawn;
            isAllUnitsSpawned = false;
            StartCoroutine(ActivateSpawnCoroutine(unitGroups, onEndSpawn));
        }
        IEnumerator ActivateSpawnCoroutine(List<UnitGroup> unitGroups, Action onEndSpawn)
        {
            foreach(UnitGroup x in unitGroups)
            {
                StartCoroutine(SpawnGroup(x.GetSpawnObjects(), x.GetUnitsDelay()));
                yield return new WaitForSeconds(x.GetGroupsDelay());
            }
            isAllUnitsSpawned = true;
            if (endWaveCondition == EndWaveCondition.whenAllUnitsWereSpawned)
            {
                onEndSpawn.Invoke();
            }
        }
        IEnumerator SpawnGroup(List<GameObject> units, float unitsDelay)
        {
            foreach (GameObject unit in units)
            {
                SpawnUnit(unit);
                yield return new WaitForSeconds(unitsDelay);
            }
        }

        void SpawnUnit(GameObject unit)
        {
            Coordinate spawnCoord = GetSpawnCoord();
            //Attention!
            PC.Spawn(unit, spawnCoord.position, spawnCoord.rotation);
        }
        Coordinate GetSpawnCoord()
        {
            return spawnRegions[UnityEngine.Random.Range(0, spawnRegions.Count)].GetSpawnCoord();
        }
        enum EndWaveCondition { whenAllUnitsWereSpawned, whenAllUnitsDied};
    }
}