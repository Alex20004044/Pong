using MSFD.SpawnSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SpawnerBase: MonoBehaviour
{
    public SpawnListBase SpawnList { get; private set; }
    public SpawnPlacerBase SpawnPlacer { get; private set; }
    public void Initializate(SpawnListBase _spawnList, SpawnPlacerBase _spawnPlacer)
    {
        SpawnList = _spawnList;
        SpawnPlacer = _spawnPlacer;
    }
    public abstract void SpawnWave(int waveInd, Action<SpawnerBase> onEndWaveCallback);

    public abstract void OnWaveIsSpawned();


}
