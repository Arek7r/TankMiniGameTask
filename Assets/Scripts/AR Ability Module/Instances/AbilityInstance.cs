using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInstance : MonoBehaviour
{
    /// <summary>
    /// invoke when Instace was activaded
    /// </summary>
    public Action abilityActivated;
    public virtual void Init()
    {
        
    }

    public virtual void Activate()
    {
        abilityActivated?.Invoke();
        //clear all events
        abilityActivated = null;
    }

}