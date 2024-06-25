using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AbilitySaves
{
    public UDictionary<string, int> abilityLevels = new UDictionary<string, int>();
    private string lastReturnedLevelName;
    
    
    public void SetLevel(AbilityDataSO ability, int level)
    {
        abilityLevels[ability.name] = level;
    }
    
    public void LevelUp(AbilityDataSO ability)
    {
        if (abilityLevels.ContainsKey(ability.name))
            SetLevel(ability, abilityLevels[ability.name] + 1);
    }
    
    public void LevelUpAll()
    {
        // ToList?
        foreach (var key in abilityLevels.Keys.ToList())
        {
            abilityLevels[key]++;
        }
    }
    
    public int GetLevel(AbilityDataSO ability)
    {
        if (ability.name == lastReturnedLevelName)
            return abilityLevels[ability.name];

        if (abilityLevels.TryGetValue(ability.name, out int level))
        {
            lastReturnedLevelName = ability.name;
            return level;
        }
        
        return 0; 
    }
   
    public int GetLevel(string abilityName)
    {
        if (abilityName == lastReturnedLevelName)
            return abilityLevels[abilityName];
        
        if (abilityLevels.TryGetValue(abilityName, out int level))
        {
            lastReturnedLevelName = abilityName;
            return level;
        }
        
        return 0; 
    }

    #region Manage Dict abilityLevels
    
    public void AddAbility(AbilityDataSO ability, int defaultLevel = 0)
    {
        abilityLevels.TryAdd(ability.name, defaultLevel);
    }

    // Usunięcie umiejętności
    public void RemoveAbility(AbilityDataSO ability)
    {
        if (abilityLevels.ContainsKey(ability.name))
        {
            abilityLevels.Remove(ability.name);
        }
    }
    
    #endregion

}