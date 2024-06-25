using System.Collections;
using System.Collections.Generic;
using AR_Ability_Module;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Ability Data", menuName = "SO Ability/Ability Data", order = 4)]
public class AbilityDataSO : ScriptableObject
{
    #region Variables
    [FormerlySerializedAs("AbilityName")] public string abilityName;
    public List<AbilityBaseSO> levels = new List<AbilityBaseSO>();
    #endregion
    
  
    public AbilityBaseSO GetAbility(int level)
    {
        return levels[level];
    }
    
    public void Use(int level, AbilityStruct structData, out AbilityInstance abilityInstance)
    {
        levels[level].Use(structData, out abilityInstance);
    }
    
    public void Use(int level = 0)
    {
        levels[level].Use();
    }

    public void Activate(int level, AbilityStruct structData)
    {
        levels[level].Activate(structData);
    }

    public float GetCooldownForLevel(int level)
    {
        return levels[level].cooldown;
    }

    public TypeActivation GetTypeActivation(int level)
    {
        return levels[level].typeActivation;
    }

    /// <summary>
    /// Checking whether the skill has no requirements. E.g. does the object have to be on the ground
    /// </summary>
    public bool CanUseAbility()
    {
        return true;
    }
}
