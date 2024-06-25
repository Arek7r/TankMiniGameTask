using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    None,
    Speed,
}

public class AbilityTypeBuff : AbilityBaseSO
{
    #region Variable
    [Space]
    //public float buffPoints;
    public float buffPercent;
    public BuffType buffType;
    #endregion

    
    public override void Use()
    {
        switch (buffType)
        {
            case BuffType.None:
                break;
            case BuffType.Speed:
                BuffSpeed();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    /// <summary>
    /// za każdym razem gdy otrzyma obrażenia to wszystkie małe kule istniejące na
    /// mapie otrzymują dodatkowe 10% prędkośc
    /// </summary>
    private void BuffSpeed()
    {
        Debug.Log("AR: BUFF SPEED for balls 10%");
        var typesToHeal = new HashSet<EnemyType> { EnemyType.Ball};
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
