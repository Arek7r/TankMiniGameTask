using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

public class SpawnAreaSimulator : MonoBehaviour
{
   public int count = 5;
   public SpawnAreaController spawnAreaController;
   public List<TransformStruct> spawnPoints = new List<TransformStruct>();
   
   // cache value
   private SpawnAreaType originalAreaType;
   
   
   private void SaveOriginal()
   {
      originalAreaType = spawnAreaController.GetAreaType();
   }
   
   private void SetOriginalType()
   {
      spawnAreaController.SetAreaType(originalAreaType);
   }
   
   /// <summary>
   /// Testing get one point
   /// </summary>
   [Button]
   private void SimulateRandomSpawnPoint()
   {
      SaveOriginal();
      spawnPoints.Clear();
      spawnPoints.Add(spawnAreaController.GetRandomPoint());
      SetOriginalType();
   }
   
   /// <summary>
   /// Testing get many points
   /// </summary>
   [Button]
   private void SimulateUniqueRandomSpawnPoints()
   {
      SaveOriginal();
   
      spawnPoints.Clear();
      spawnPoints.AddRange(spawnAreaController.GetRandomPoints(count));
      SetOriginalType();
   }
   
   /// <summary>
   /// Testing get points in shape
   /// </summary>
   [Button]
   private void SimulateInCircle()
   {
      SaveOriginal();
      spawnAreaController.SetAreaType(SpawnAreaType.InCircle);
   
      spawnPoints.Clear();
      spawnPoints.AddRange(spawnAreaController.GetRandomPoints(count)); 
   
      SetOriginalType();
   }
   
   
   
   private void OnDrawGizmos()
   {
      if (spawnPoints == null || spawnPoints.Count == 0)
         return;
   
      Gizmos.color = Color.red;
   
      for (int i = 0; i < spawnPoints.Count; i++)
      {
            Gizmos.DrawSphere(spawnPoints[i].position, 0.3f);
      }
   }
}
