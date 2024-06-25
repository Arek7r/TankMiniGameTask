using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using _AR_.Extensions;
using TriInspector;
using UltraPool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class SpawnManager : MonoBehaviour
{
    #region Variable

    public bool autoStart;

    [InlineEditor]
    public SpawnerData spawnerData;
    //public List<Transform> spawnPoints = new List<Transform>();
    public List<SpawnAreaController> spawnArea;
    
    // cache variables
    private TransformStruct spawnPoint;
    private ObjectSpawnInfo randomEnemy;
    private Character enemy;
    private float nextSpawnTime = 0f;

    [Title("Debug")]
    public WaveData currWaveData;
    public int currentWaveNumber = 1;
    public float elapsedTime;
    public float duration;
    public int spawnedObjects;
    private Coroutine currentSpawnCoroutine;

    #region Events
    public event Action OnSpawnStarted;
    public event Action OnSpawnStopped;
    public event Action OnWaveCompleted;
    public event Action OnAllWavesCompleted;
    #endregion
    
    #endregion

    private void Start()
    {
        SetWaveData();

        if (autoStart)
            StartWave();
    }

    private void StartWave()
    {
        switch (currWaveData.waveEndCondition)
        {
            // case WaveEndCondition.None:
            //     currentSpawnCoroutine = StartCoroutine(SpawnByCount());
            //     break;
            case WaveEndCondition.Time:
                currentSpawnCoroutine = StartCoroutine(SpawnByTime());
                break;
            case WaveEndCondition.SpawnedCount:
                currentSpawnCoroutine = StartCoroutine(SpawnByCount());
                break;
            case WaveEndCondition.ExternalEvent:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void StopWave()
    {
        if (currentSpawnCoroutine != null)
        {
            StopCoroutine(currentSpawnCoroutine);
            currentSpawnCoroutine = null;
            OnSpawnStopped?.Invoke();
        }
    }
    
    
    private void StartNextWave()
    {
        if (currentWaveNumber +1 <= spawnerData.GetMaxWaveCount() )
        {
            currentWaveNumber++;
            SetWaveData();
            StartWave();       
        }
        else
        {
            // all waves completed
            OnAllWavesCompleted?.Invoke();
        }
    }

    #region SpawnTypes
    private IEnumerator SpawnByTime()
    {
        Debug.Log("1");
        if (currWaveData.spawnStartDelay != 0)
            yield return new WaitForSeconds(currWaveData.spawnStartDelay);

        Debug.Log("AR: 2");
        elapsedTime = 0f;
        nextSpawnTime = 0f;
        OnSpawnStarted?.Invoke();
        
        duration = currWaveData.spawnDurationMinutes * 60f + currWaveData.spawnDurationSeconds;

        Debug.Log("3");
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= nextSpawnTime)
            {
                SpawnObject();
                nextSpawnTime += currWaveData.spawnEverySecond;
            }

            yield return null;
        }
        
        OnWaveCompleted?.Invoke();
        //ToDo next delay?
        if (currWaveData.autoStartNextWave)
            StartNextWave();
    }
 
    private IEnumerator SpawnByCount()
    {
        if (currWaveData.spawnStartDelay != 0)
            yield return new WaitForSeconds(currWaveData.spawnStartDelay);

        elapsedTime = 0f;
        nextSpawnTime = 0f;
        OnSpawnStarted?.Invoke();

        while (spawnedObjects < currWaveData.requiredSpawnCount)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= nextSpawnTime)
            {
                SpawnObject();
                nextSpawnTime += currWaveData.spawnEverySecond;
            }

            yield return null;
        }
        
        OnWaveCompleted?.Invoke();
        //ToDo next delay?
        if (currWaveData.autoStartNextWave)
            StartNextWave();
    }
    
    #endregion

    private void SpawnObject()
    {
        randomEnemy = spawnerData.GetRandomObject(currentWaveNumber);
        spawnPoint = GetSpawnPoint();

        if (randomEnemy != null)
        {
            enemy = ObjectPoolManager.Instance.GetObjectAuto<Enemy>(randomEnemy.prefab);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            
            enemy.Init();
            enemy.gameObject.SetActive(true);
            enemy = null;
            
            spawnedObjects++;
        }
        else
        {
            Debug.LogError("Object is not spawned");
        }
    }

    #region Get

    public int GetCurrentWaveNumber()
    {
        return currentWaveNumber;
    }

    private TransformStruct GetSpawnPoint()
    {
        // If there is only one spawn area, always use it.
        if (spawnArea.Count == 1)
        {
            return spawnArea[0].GetRandomPoint();
        }

        // If the current wave number is within the range of available spawn areas,
        // use the corresponding spawn area (1-based index).
        if (currentWaveNumber <= spawnArea.Count)
        {
            return spawnArea[currentWaveNumber - 1].GetRandomPoint();
        }

        // If the current wave number exceeds the number of spawn areas,
        // use the last available spawn area.
        return spawnArea[spawnArea.Count - 1].GetRandomPoint();
    }

    #endregion

    #region Set

    private void SetWaveData()
    {
        currWaveData = spawnerData.GetWaveData(currentWaveNumber);
    }

    #endregion
}