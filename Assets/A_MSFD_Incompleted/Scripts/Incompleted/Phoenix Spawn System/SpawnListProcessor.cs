using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.PhoenixSpawnSystem
{
    public class SpawnListProcessor : MonoBehaviour
    {
        [Sirenix.OdinInspector.Button]
        public static List<UnitGroup> GetUnitsGroups(SpawnList spawnList, int waveInd, string[] tags = null)
        {
            Wave wave = spawnList.waves[waveInd];

            List<Unit> allUnits = new List<Unit>();
            int totalUnitsCount = 0;

            for (int i = 0; i < spawnList.units.Count; i++)
            {
                if (tags == null || tags.Length == 0 || AuxiliarySystem.CompareTags(tags, spawnList.units[i].GetTags()))
                {
                    allUnits.Add(new Unit(spawnList.units[i].GetPrefab(), wave.unitsNum[i]));
                    totalUnitsCount += wave.unitsNum[i];
                }
            }
            
            
            List<UnitGroup> unitGroups = new List<UnitGroup>();
            int unreadyUnits = totalUnitsCount;
            for(int i =0; i < wave.GroupsInWave; i++)
            {
                int unitsCount;
                if(i == wave.GroupsInWave - 1)
                {
                    unitsCount = unreadyUnits;
                }
                else
                {
                    unitsCount = (int) (totalUnitsCount * spawnList.unitsPerGroupAllocation.unitsAllocation[wave.GroupsInWave - 1].groupProcent[i]);
                    unreadyUnits -= unitsCount;
                }
                unitGroups.Add(new UnitGroup(unitsCount, wave.unitsDelay, wave.groupsDelay));
            }


            List<UnitGroup> finallGroups = new List<UnitGroup>();
            for (int i = 0; i < totalUnitsCount;)
            {
                int unitInd = AuxiliarySystem.GetRandomIndex(allUnits.Count);
                Unit unit = allUnits[unitInd];
                if (unit.count <= 0)
                {
                    allUnits.RemoveAt(unitInd);
                    continue;
                }

                int groupInd = AuxiliarySystem.GetRandomIndex(unitGroups.Count);
                unitGroups[groupInd].TryAddUnit(unit.prefab);
                
                unit.count--;

                if(unitGroups[groupInd].IsGroupReady())
                { 

                    finallGroups.Add(unitGroups[groupInd]);
                    unitGroups.RemoveAt(groupInd);
                }
                i++;
            }
            /*
            int c = 1;
            for(int i =0; i < finallGroups.Count; i++)
            {
                Debug.Log("---Group: " + i);
                foreach(GameObject go in finallGroups[i].GetSpawnObjects())
                {
                    Debug.Log(go.name + " " + c);
                    c++;
                }
            }
            Debug.Log("SSSSSSSSSSSSS");*/
            /*
            for (int i = 0; i < finallGroups.Count; i++)
            {
                Debug.Log("---Group: " + i);
                foreach (GameObject go in finallGroups[i].GetSpawnObjects())
                {
                    Debug.Log(go.name);
                    
                }
            }*/

            return finallGroups;
        }
        public static string anyTag = "ANY_TAG";
    }

    public class UnitGroup
    {
        List<GameObject> unitsPrefabs = new List<GameObject>();
        int targetUnitCount;
        float unitsDelay;
        float delayBetweenGroups;
        public UnitGroup(int targetUnitCount, float unitsDelay, float delayBetweenGroups)
        {
            this.targetUnitCount = targetUnitCount;
            this.unitsDelay = unitsDelay;
            this.delayBetweenGroups = delayBetweenGroups;
        }

        public bool TryAddUnit(GameObject prefab)
        {
            if (unitsPrefabs.Count < targetUnitCount)
            {
                unitsPrefabs.Add(prefab);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsGroupReady()
        {
            return unitsPrefabs.Count == targetUnitCount;
        }
        public List<GameObject> GetSpawnObjects()
        {
            return unitsPrefabs;
        }
        public float GetUnitsDelay()
        {
            return unitsDelay;
        }
        public float GetGroupsDelay()
        {
            return delayBetweenGroups;
        }
    }
    public class Unit
    {
        public GameObject prefab;
        public int count;

        public Unit(GameObject unit, int count)
        {
            this.prefab = unit;
            this.count = count;
        }
    }

}