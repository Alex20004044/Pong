using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MSFD.PhoenixSpawnSystem
{
    public class SpawnPlacerBonus : SpawnPlacerBase
    {
        [SerializeField]
        float delayBetweenBonuses = 10f;

        [SerializeField]
        bool isAutoSetPoints = true;
        //!
        [Sirenix.OdinInspector.HideIf("isAutoSetPoints")]
        [SerializeField]
        List<SpawnRegionClamp> spawnRegions;

        Action onEndSpawn;

        Queue<GameObject> bonusesToSpawn = new Queue<GameObject>();

        public static string BONUS_PICKED = "BONUS_PICKED";
        private void Awake()
        {
            if (isAutoSetPoints)
            {
                spawnRegions.Clear();
                foreach (Transform s in transform)
                {
                    spawnRegions.Add(new SpawnRegionClamp(s.GetComponent<SpawnRegionBase>(), 1));
                }
            }
        }
        public override void ActivateSpawn(List<UnitGroup> unitGroups, Action onEndSpawn)
        {
            StopAllCoroutines();
            this.onEndSpawn = onEndSpawn;
            AddBonusesToQueue(unitGroups);
            onEndSpawn.Invoke();
        }

        void AddBonusesToQueue(List<UnitGroup> unitGroups)
        {
            foreach (UnitGroup ug in unitGroups)
            {
                foreach (GameObject unit in ug.GetSpawnObjects())
                {
                    bonusesToSpawn.Enqueue(unit);
                }
            }
            StartCoroutine(ActivateSpawnCoroutine());
        }

        IEnumerator ActivateSpawnCoroutine()
        {
            while(bonusesToSpawn.Count > 0)
            {
                if (TrySpawnBonus(bonusesToSpawn.Peek()))
                {
                    bonusesToSpawn.Dequeue();
                }
                yield return new WaitForSeconds(delayBetweenBonuses);
            }
        }

        bool TrySpawnBonus(GameObject unit)
        {
            Coordinate spawnCoord;
            var emptySpawnRegion = AuxiliarySystem.GetRandomDataList<SpawnRegionClamp>(spawnRegions, 1,
                   (SpawnRegionClamp x) => { return x.IsHasFreeSpace(); }, false, false);
            if (emptySpawnRegion != null)
            {
                SpawnRegionClamp spawnRegionClamp = emptySpawnRegion[0];
                spawnRegionClamp.AddSpawnedUnit();
                spawnCoord = spawnRegionClamp.SpawnRegion.GetSpawnCoord();
                int spawnRegionInd = spawnRegions.IndexOf(spawnRegionClamp);
                //Attention!
                GameObject go;
                go = PC.Spawn(unit, spawnCoord.position, spawnCoord.rotation);

                go.transform.SetParent(spawnRegionClamp.SpawnRegion.transform);
                UnityEventOnDisable unityEventOnDisable = go.AddComponent<UnityEventOnDisable>();
                unityEventOnDisable.AddListener(() => OnBonusPicked(spawnRegionInd, unityEventOnDisable));
                return true;
            }
            else
            {
                return false;
            }          
        }
        void OnBonusPicked(int spawnRegionInd, UnityEventOnDisable unityEventOnDisable)
        {
            spawnRegions[spawnRegionInd].RemoveSpawnedUnit();
            unityEventOnDisable.RemoveListener(() => OnBonusPicked(spawnRegionInd, unityEventOnDisable));
            Destroy(unityEventOnDisable);
        }
    }
    [System.Serializable]
    public class SpawnRegionClamp
    {
        public SpawnRegionBase SpawnRegion;
        public int MaxUnitsAmount = 1;

        int currentUnits = 0;
        public SpawnRegionClamp(SpawnRegionBase _spawnRegion, int _maxUnitsAmount)
        {
            SpawnRegion = _spawnRegion;
            MaxUnitsAmount = _maxUnitsAmount;
        }
        public void AddSpawnedUnit()
        {
            currentUnits++;
        }
        public void RemoveSpawnedUnit()
        {
            currentUnits--;
        }
        public bool IsHasFreeSpace()
        {
            return (currentUnits < MaxUnitsAmount);
        }
    }

}