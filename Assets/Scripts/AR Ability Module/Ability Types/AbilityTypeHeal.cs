using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealType
{
    normal,
    Add10PercentForCubes,
    HealFor50Below,
}

[CreateAssetMenu(fileName = "ability Type Heal", menuName = "SO Ability/Heal", order = 3)]
public class AbilityTypeHeal : AbilityBaseSO
{
    [Space]
    //public float healPoints;
    public float healPercent;
    public HealType healType;
    
    public override void Use()
    {
        switch (healType)
        {
            case HealType.normal:
                break;
            case HealType.Add10PercentForCubes:
                Heal10Percent();
                break;
            case HealType.HealFor50Below:
                FullHealFor50Below();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// all existing hexes on the map (including large hexes) receive 10% of their maximum health
    /// </summary>
    private void Heal10Percent()
    {
        // types of enemy get heal
        var typesToHeal = new HashSet<EnemyType> { EnemyType.Cube, EnemyType.CubeBig };
        foreach (var enemyPair in EnemyManager.Instance.GetEnemyDict())
        {
            if (typesToHeal.Contains(enemyPair.Key))
            {
                foreach (var enemy in enemyPair.Value)
                {
                    enemy.AddHP(enemy.characterConfig.HpMax * healPercent);
                }
            }
        }
    }

    /// <summary>
    /// each time it dies, it renews all health for all opponents who have less than 50% health
    /// </summary>
    private void FullHealFor50Below()
    {
        Debug.Log("AR: Heal FULL HP if enemy < 50");
        foreach (var enemyPair in EnemyManager.Instance.GetEnemyDict())
        {
            foreach (var enemy in enemyPair.Value)
            {
                if (enemy.GetCurrentPercentHP0100() < 50)
                {
                    enemy.AddMaxHPPercent0100(healPercent);
                }
            }
        }
    }
}
