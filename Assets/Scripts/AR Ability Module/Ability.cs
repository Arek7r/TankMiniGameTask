using System;
using System.Collections;
using System.Collections.Generic;
using AR_Ability_Module;
using TriInspector;
using UnityEngine;

[System.Serializable]
public class Ability
{
    public AbilityDataSO abilityData;
    private AbilityCooldown abilityCooldown;
    private AbilityStruct abilityStruct;
    private TypeActivation activationType;
    public Action abilityActivated;

    private int currentlvl;
    
    // public for debugs in inspector
    // Reference to the current object that was created when the skill was invoked
    [DisableInEditMode]
    public AbilityInstance abilityInstance; 
    [DisableInEditMode]
    public AbilityState abilityState;
 
    
    
    public void Init(Transform owner)
    {
        abilityStruct = new AbilityStruct
        {
            spawnPoint = FindSpawnPoint(owner),
            owner = owner
        };
        
        this.abilityCooldown = new AbilityCooldown();
        abilityCooldown.Init();
        abilityCooldown.SetCooldown(abilityData.GetCooldownForLevel(currentlvl));
    }

    public int GetCurrentLvl()
    {
        return currentlvl;
    }

    public AbilityDataSO GetAbilityData()
    {
        return abilityData;
    }

    public bool CanUseAbility()
    {
        // we can put here move conditions
        return abilityData.CanUseAbility() && abilityCooldown.CanUseAbility();
    }
    
    /// <summary>
    /// Each skill can have a different SpawnPoint and each owner can have a different SpawnPoint.
    /// </summary>
    private Transform FindSpawnPoint(Transform owner)
    {
        // for our simple test case 
        return owner.GetComponent<TurretGun>().GetSpawnPoint();
    }
    
    
    public void UseAbility()
    {
        activationType = abilityData.GetTypeActivation(currentlvl);

        if (activationType == TypeActivation.Instant)
        {
            abilityState = AbilityState.Normal;
            abilityData.Use(currentlvl, abilityStruct, out abilityInstance);
        }
        else if (activationType == TypeActivation.TwoStep)
        {
            if (abilityState == AbilityState.Normal)
                PrepareAbilityToActive(abilityStruct);
            else
                ActivateAbility();
        }
    }
    
    private void PrepareAbilityToActive(AbilityStruct structData)
    {
        abilityState = AbilityState.WaitingForActivation;
        abilityData.Use(currentlvl, structData, out abilityInstance);
        abilityInstance.abilityActivated += AbilityActivated;
    }

    private void ActivateAbility()
    {
        abilityInstance.Activate();
        abilityCooldown.StartCooldown();
    }
    
    private void AbilityActivated()
    {
        if (abilityInstance != null)
            abilityInstance.abilityActivated -= AbilityActivated;

        abilityCooldown.StartCooldown();
        abilityState = AbilityState.Normal;
        abilityInstance = null;
    }

    public void UpdateCooldown()
    {
        abilityCooldown.UpdateCooldown();
    }

    public void LevelUp()
    {
        currentlvl++;
        abilityCooldown.ResetCooldown(); 
        abilityCooldown.SetCooldown(abilityData.GetCooldownForLevel(currentlvl));
    }
}
