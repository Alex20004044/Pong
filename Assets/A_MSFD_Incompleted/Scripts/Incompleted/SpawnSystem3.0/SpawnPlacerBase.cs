using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.SpawnSystem
{
    [System.Serializable]
    public class SpawnPlacerBase : MonoBehaviour
    {
        [SerializeField]
        Transform[] spawnPoints;

        /*
        public void SpawnGroup(SpawnerTest.UnitGroup unitGroup)
        {
            
        }*/
        public IEnumerator SpawnUnit(GameObject unit, int count, float delay)
        {

            for( int i = 0; i < count; i++)
            {
                Instantiate(unit);
                yield return new WaitForSeconds(delay);
            }
            
            //PoolManager.instance.Instantiate(unit, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }
}