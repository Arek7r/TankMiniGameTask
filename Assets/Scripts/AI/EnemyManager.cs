using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    #region Variables
    // Dictionary because it does not provide for large amounts of data.
    private Dictionary<EnemyType, List<Enemy>> enemyEnabledDict = new Dictionary<EnemyType, List<Enemy>>();
    #endregion

    #region Get

    public Dictionary<EnemyType, List<Enemy>> GetEnemyDict()
    {
        return enemyEnabledDict;
    }

    #endregion
    
    public void RegisterEnemy(Enemy enemy)
    {
        if (enemyEnabledDict.ContainsKey(enemy.enemyType) == false)
            enemyEnabledDict.Add(enemy.enemyType, new List<Enemy>());
        
        enemyEnabledDict[enemy.enemyType].Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        if (enemyEnabledDict.ContainsKey(enemy.enemyType))
            enemyEnabledDict[enemy.enemyType].Remove(enemy);
    }

    public void OnEnemyDestroyed(Enemy enemy)
    {}
}
