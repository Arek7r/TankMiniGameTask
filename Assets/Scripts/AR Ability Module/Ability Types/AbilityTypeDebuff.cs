using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebuffType
{
    None,
    Speed,
}
public class AbilityTypeDebuff : AbilityBaseSO
{
    #region Variable
    [Space]
    //public float buffPoints;
    public float buffPercent;
    public DebuffType debuffType;
    #endregion

    
    public override void Use()
    {
        switch (debuffType)
        {
            case DebuffType.None:
                break;
            case DebuffType.Speed:
                DebuffSpeed();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// each time it takes damage, all bullets (including small bullets) currently on the map lose 10% of their current speed
    /// </summary>
    private void DebuffSpeed()
    {
        Debug.Log("AR: DebuffSpeed Activated");
        var typesToHeal = new HashSet<EnemyType> { EnemyType.Ball, EnemyType.BallBig};
        foreach (var enemyPair in EnemyManager.Instance.GetEnemyDict())
        {
            if (typesToHeal.Contains(enemyPair.Key))
            {
                foreach (var enemy in enemyPair.Value)
                {
                    enemy.AddSpeedMaxPercent0100(buffPercent);
                }
            }
        }
    }
}
