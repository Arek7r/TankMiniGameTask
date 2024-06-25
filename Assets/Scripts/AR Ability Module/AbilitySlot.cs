using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AbilitySlot
{
    public Ability ability;
    
    public AbilitySlot(Ability ability)
    {
        this.ability = ability;
    }
    
    /// <summary>
    /// Use the ability 
    /// </summary>
    public void UseAbility()
    {
        if (ability.CanUseAbility())
        {
            ability.UseAbility();
        }
        else
        {
            Debug.Log("AR: CANNOT USE ");
            // ToDo add audio / UI info
        }
    }

   
}
