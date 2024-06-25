using System;
using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

#region AbilityStruct
/// <summary>
/// Container for all needed data. It's cheap to transfer data so why not.
/// We do not know how much data we will need in the future for this system
/// </summary>
public struct AbilityStruct
{
    public Transform owner;
    public Transform spawnPoint ;
    public AbilityCooldown abilityCooldown;
}
#endregion

public class AbilityManager : MonoBehaviour
{
    #region Variable
   // public AbilityCharacter abilityCharacter; // Current abilities
    //public AbilitySaves abilitySaves;         // For save abilities state/status
    
    //public List<AbilityCooldown> abilityCooldowns = new List<AbilityCooldown>();
    public List<Ability> availableAbilities = new List<Ability>();
    public List<AbilitySlot> abilitySlotList = new List<AbilitySlot>();
    #endregion

    private void Awake()
    {
        LoadData();
        InitAbilities();
    }

    public void SaveData()
    {}

    /// <summary>
    /// LoadData from XML/JSON/Prefs
    /// </summary>
    private void LoadData()
    {
        // availableAbilities <- load here saved data
        
        // for demo: we load the first skill what we gave in the inspector
        abilitySlotList.Add(new AbilitySlot(availableAbilities[0]));
    }

    public void AddAbility(Ability ability)
    { }

    public void AddAbilityToSlot(Ability ability, int slotIndex)
    {
        // remove previous
        // add new to slots
        // init ability
    }

    private void InitAbilities()
    {
        foreach (var ability in availableAbilities)
        {
            ability.Init(transform);
        }
    }

    
    void Update()
    {
        // ToDo:
        // If game is not int pause state
        
        foreach (var slot in abilitySlotList)
        {
            if (slot != null)
                slot.ability.UpdateCooldown();
        }
    }

    /// <summary>
    /// Use the ability in the indicated slot
    /// </summary>
    /// <param name="slotIndex"></param>
    public void UseAbilitySlot(int slotIndex)
    {
        if (abilitySlotList.Count > slotIndex + 1 || slotIndex < 0)
            return;
        
        abilitySlotList[slotIndex].UseAbility();
    }
    
    
    //
    // /// <summary>
    // /// Change of abilities in the selected slot
    // /// </summary>
    // /// <param name="slotIndex"></param>
    // /// <param name="newAbility"></param>
    // public void ChangeAbilityInSlot(int slotIndex, AbilityDataSO newAbility)
    // {
    //     if (slotIndex >= 0 && slotIndex < abilityCharacter.currentAbilities.Count)
    //     {
    //         abilityCharacter.currentAbilities[slotIndex] = newAbility;
    //         abilityCooldowns[slotIndex] = new AbilityCooldown(newAbility);
    //     }
    // }
    


    // /// <summary>
    // /// increases skill level
    // /// </summary>
    // /// <param name="ability"></param>
    // public void LevelUpAbility(AbilityDataSO ability)
    // {
    //     abilitySaves.LevelUp(ability);
    // }

    
    [Button]
    private void TestAbility()
    {
        UseAbilitySlot(0);
    }
}
