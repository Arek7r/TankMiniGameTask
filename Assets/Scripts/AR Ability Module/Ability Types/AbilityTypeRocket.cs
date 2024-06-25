using System.Collections;
using System.Collections.Generic;
using UltraPool;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ability Type Rocket", menuName = "SO Ability/Rocket", order = 3)]
public class AbilityTypeRocket : AbilityBaseSO
{
    #region Variable

    public RocketInstance rocketPrefab;
    public Transform explosionPrefab;
    private RocketInstance rocketPrefabInstance;
    
    public float damage;
    [FormerlySerializedAs("explosionRange")] 
    public float explosionRadius;
   
    #endregion


    public override void Use(AbilityStruct abilityStruct, out AbilityInstance abilityInstance)
    {
        Debug.Log("AR:  USE ROCKET");
        rocketPrefabInstance = ObjectPoolManager.Instance.GetObjectAuto(rocketPrefab);
        rocketPrefabInstance.gameObject.SetActive(false);
        
        rocketPrefabInstance.transform.SetNewPosRot(abilityStruct.spawnPoint);
        rocketPrefabInstance.owner = abilityStruct.owner;
        rocketPrefabInstance.explodePrefab = explosionPrefab; 
        rocketPrefabInstance.damageValue = damage; 
        rocketPrefabInstance.explosionRadius = explosionRadius; 

        rocketPrefabInstance.Init();
        rocketPrefabInstance.gameObject.SetActive(true);
       
        abilityInstance = rocketPrefabInstance;
    }
    
    
   
}
