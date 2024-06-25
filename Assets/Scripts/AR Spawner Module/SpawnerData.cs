using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Spawner Data", menuName = "SO Spawner/Spawner Data", order = 1)]
public class SpawnerData : ScriptableObject
{
    [SerializeField] public List<WaveData> waveDataList = new List<WaveData>();

    public int GetMaxWaveCount()
    {
        return waveDataList.Count();
    }

    public ObjectSpawnInfo GetRandomObject(int waveID)
    {
        return waveDataList[waveID - 1].GetRandomObject();
    }

    public WaveData GetWaveData(int waveID)
    {
        return waveDataList[waveID - 1];
    }

    // public WaveData GetWaveData(int waveIndex)
    // {
    //     return waveDataList[waveIndex];
    // }

    public void CreateNewWave()
    {
        waveDataList.Add(new WaveData
        {
            waveName = "Wave " + (waveDataList.Count + 1),
            objectsToSpawn = null,
            requiredSpawnCount = 1,
            spawnEverySecond = 1,
            spawnStartDelay = 0
        });
    }

    public void RemoveLastWave()
    {
        waveDataList.RemoveAt(waveDataList.Count);
    }
}

[Serializable]
public class WaveData
{
    public string waveName;
    public WaveEndCondition waveEndCondition;
    public List<ObjectSpawnInfo> objectsToSpawn = new List<ObjectSpawnInfo>();

    public bool autoStartNextWave = true;
    public float spawnStartDelay;
    public float spawnEverySecond;

    public int requiredSpawnCount;

    public float spawnDurationMinutes;
    public float spawnDurationSeconds;


    public void AddObjectToSpawn(GameObject go)
    {
        if (objectsToSpawn == null)
            objectsToSpawn = new List<ObjectSpawnInfo>();
        
        objectsToSpawn.Add(new ObjectSpawnInfo(go, 100));
    }

    public void RemoveLast()
    {
        if(objectsToSpawn.Count >= 1)
            objectsToSpawn.RemoveAt(objectsToSpawn.Count - 1);
    }
    
    public ObjectSpawnInfo GetRandomObject()
    {
        int totalChance = objectsToSpawn.Sum(enemy => enemy.spawnChance);
        int randomPoint = Random.Range(0, totalChance);
        int currentSum = 0;

        foreach (var objects in objectsToSpawn)
        {
            currentSum += objects.spawnChance;
            if (randomPoint <= currentSum)
                return objects;
        }

        return null;
    }

    public void CopyPropertiesFrom(WaveData otherWaveData)
    {
        waveEndCondition = otherWaveData.waveEndCondition;
        autoStartNextWave = otherWaveData.autoStartNextWave;
        spawnStartDelay = otherWaveData.spawnStartDelay;
        spawnEverySecond = otherWaveData.spawnEverySecond;
        requiredSpawnCount = otherWaveData.requiredSpawnCount;
        spawnDurationMinutes = otherWaveData.spawnDurationMinutes;
        spawnDurationSeconds = otherWaveData.spawnDurationSeconds;
    }
}

[System.Serializable]
public class ObjectSpawnInfo
{
    public GameObject prefab;
    [Range(0, 100)] public int spawnChance = 100;

    public ObjectSpawnInfo(GameObject go, int chance)
    {
        prefab = go;
        spawnChance = chance;
    }
}

public enum WaveEndCondition
{
    //None,
    Time,
    SpawnedCount,
    ExternalEvent,
}