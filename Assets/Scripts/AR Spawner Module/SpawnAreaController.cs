using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public struct TransformStruct
{
    public Vector3 position;
    public Quaternion rotation;

    public TransformStruct(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}

public enum SpawnAreaType
{
    Points,
    OnCircle,
    InCircle,
    OnLineHorizontal,
    OnLineVertical,
}

public class SpawnAreaController : MonoBehaviour
{
    #region Variables

    public SpawnAreaType areaType;
    public List<Transform> spawnPoints;
    public float minDistanceBetweenPoints;

    [Space]

    public float radiusCircle;
    public float lineLength;
    private int attemptsMax = 10;

    // cache for avoid gc
    private List<TransformStruct> randomPointsStruct = new List<TransformStruct>();
    private Transform previousPoint;
    private int randomIndex;
    private Transform randomTransform;
    private int attempts;

    #endregion


    public SpawnAreaType GetAreaType()
    {
        return areaType;
    }
    
    public TransformStruct GetRandomPoint()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("Spawn points list is empty or null");
            return new TransformStruct();
        }

        randomIndex = Random.Range(0, spawnPoints.Count);

        if (areaType == SpawnAreaType.InCircle)
        {
            return GetPointInCircle();
        }
        
        return new TransformStruct
        {
            position = spawnPoints[randomIndex].position,
            rotation = spawnPoints[randomIndex].rotation
        };
    }

    public List<TransformStruct> GetRandomPoints(int numberOfPoints)
    {
        switch (areaType)
        {
            case SpawnAreaType.InCircle:
                return GetPointsInCircle(numberOfPoints);
            case SpawnAreaType.Points:
            case SpawnAreaType.OnCircle:
            case SpawnAreaType.OnLineHorizontal:
            case SpawnAreaType.OnLineVertical:
                return GetPointsFromSpawnPoints(numberOfPoints);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private List<TransformStruct> GetPointsFromSpawnPoints(int count)
    {
        List<TransformStruct> result = new List<TransformStruct>();

        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("Spawn points list is empty or null");
            return result;
        }

        if (count <= 0)
        {
            Debug.LogError("Count must be greater than 0");
            return result;
        }

        previousPoint = null;

        for (int i = 0; i < count; i++)
        {
            randomTransform = null;
            attempts = 0;

            while (attempts < attemptsMax)
            {
                randomTransform = GetRandomPointFromSpawnPoints();
                if (randomTransform != previousPoint && IsFarEnough(result, randomTransform.position))
                {
                    break;
                }
                attempts++;
            }

            result.Add(new TransformStruct
            {
                position = randomTransform.position,
                rotation = randomTransform.rotation
            });
            previousPoint = randomTransform;
        }

        return result;
    }

    private Transform GetRandomPointFromSpawnPoints()
    {
        randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }

    private List<TransformStruct> GetPointsInCircle(int count)
    {
        List<TransformStruct> randomPoints = new List<TransformStruct>();
        Vector3 centerPosition = transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 randomPoint = Vector3.zero;
            attempts = 0;

            while (attempts < attemptsMax)
            {
                // Generate random angle and distance
                float angle = Random.Range(0f, Mathf.PI * 2);
                float distance = Random.Range(0f, radiusCircle);

                // Calculate point position
                float x = centerPosition.x + distance * Mathf.Cos(angle);
                float z = centerPosition.z + distance * Mathf.Sin(angle);
                randomPoint = new Vector3(x, centerPosition.y, z);

                if (IsFarEnough(randomPoints, randomPoint))
                {
                    break;
                }
                attempts++;
            }

            randomPoints.Add(new TransformStruct
            {
                position = randomPoint,
                rotation = Quaternion.identity
            });
        }

        return randomPoints;
    }
    
    private TransformStruct GetPointInCircle()
    {
        Vector3 centerPosition = transform.position;

        // Generate random angle and distance
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, radiusCircle);

        // Calculate point position
        float x = centerPosition.x + distance * Mathf.Cos(angle);
        float z = centerPosition.z + distance * Mathf.Sin(angle);
        Vector3 randomPoint = new Vector3(x, centerPosition.y, z);

        return new TransformStruct(position: randomPoint, rotation: Quaternion.identity);
    }

    private bool IsFarEnough(List<TransformStruct> points, Vector3 newPoint)
    {
        foreach (TransformStruct point in points)
        {
            if (Vector3.Distance(point.position, newPoint) < minDistanceBetweenPoints)
            {
                return false;
            }
        }
        return true;
    }

    public void SetAreaType(SpawnAreaType newAreaType )
    {
        areaType = newAreaType;
    }

    [Button]
    public void SetSpawnPointsInCircle()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("Spawn points list is empty or null");
            return;
        }

        float angleStep = 360f / spawnPoints.Count;
        Vector3 centerPosition = transform.position;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = centerPosition.x + radiusCircle * Mathf.Cos(angle);
            float z = centerPosition.z + radiusCircle * Mathf.Sin(angle);
            Vector3 spawnPosition = new Vector3(x, centerPosition.y, z);

            spawnPoints[i].position = spawnPosition;
        }
    }

    [Button]
    public void SetSpawnPointsInLine()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("Spawn points list is empty or null");
            return;
        }

        float spacing = lineLength / (spawnPoints.Count - 1);
        Vector3 centerPosition = transform.position;
    
        switch (areaType)
        {
            case SpawnAreaType.OnLineVertical:
                float startX = centerPosition.x - (lineLength / 2);

                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    float x = startX + i * spacing;
                    Vector3 spawnPosition = new Vector3(x, centerPosition.y, centerPosition.z);
                    spawnPoints[i].position = spawnPosition;
                }
                break;
            
            case SpawnAreaType.OnLineHorizontal:
                float startZ = centerPosition.z - (lineLength / 2);

                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    float z = startZ + i * spacing;
                    Vector3 spawnPosition = new Vector3(centerPosition.x, centerPosition.y, z);
                    spawnPoints[i].position = spawnPosition;
                }
                break;

            default:
                Debug.LogError("Invalid area type for setting spawn points in line");
                break;
        }
    }


    [Button]
    public void SetChildrenAsSpawnPoints()
    {
        // Clear the existing spawn points list
        spawnPoints.Clear();

        // Loop through all children of the current GameObject
        foreach (Transform child in transform)
        {
            // Add each child to the spawn points list
            spawnPoints.Add(child);
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
            return;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i] != null)
            {
                // Draw line to the next spawn point
                Gizmos.color = Color.red;
                Transform nextPoint = spawnPoints[(i + 1) % spawnPoints.Count];
                if (nextPoint != null)
                {
                    Gizmos.DrawLine(spawnPoints[i].position, nextPoint.position);
                }

                Gizmos.color = Color.blue;
                if (spawnPoints[i] != null)
                {
                    Gizmos.DrawSphere(spawnPoints[i].position, 0.3f);
                }
            }
        }
    }
}
