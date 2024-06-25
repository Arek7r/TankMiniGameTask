using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _AR_.Extensions;
using _Scripts_.Bullets_Projectiles;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "Turret Gun Config SO", menuName = "SO Turret/Gun Config", order = 3)]
public class TurretGunConfigSO : ScriptableObject
{
    #region Variables

    public string configName;
    public List<TurretGunLevel> levels= new List<TurretGunLevel>();

    #endregion

    #region Methods
    /// <summary>
    /// Returns data of the requested level
    /// </summary>
    /// <param name="currLvl"></param>
    /// <returns></returns>
    public TurretGunLevel GetData(int currLvl)
    {
        if (currLvl >= levels.Count)
        {
            Debug.LogError("AR: Request for a non-existent level");
            return levels[levels.Count-1];
        }
        
        return levels[currLvl];
    }
    
    //Only editor - 
    public void AddLevelEditor()
    {
    }

    #endregion
}

