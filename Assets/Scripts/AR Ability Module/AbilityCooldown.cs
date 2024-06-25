using System;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class AbilityCooldown
{
    #region Variables

    private float cooldown;
    public float remainingCooldown;
    
    #endregion

    public void Init()
    {
        this.remainingCooldown = 0f;
    }

    public void UpdateCooldown()
    {
        if (remainingCooldown > 0f)
        {
            remainingCooldown -= Time.deltaTime;
        }
    }

    public bool CanUseAbility()
    {
        return remainingCooldown <= 0f;
    }

    public void ResetCooldown()
    {
        this.remainingCooldown = 0f;
    }
    
    public void StartCooldown()
    {
        remainingCooldown = cooldown;
    }

    public void SetCooldown(float newCooldown)
    {
        cooldown = newCooldown;
    }
}