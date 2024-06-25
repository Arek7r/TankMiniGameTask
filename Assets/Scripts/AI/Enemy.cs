using System;
using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UltraPool;
using UnityEngine;

/// <summary>
/// EnemyType jako enum bo po doswiadczeniach w projekcie Rustler wiem ze zmiany "klasy postaci" w locie jest przydatne, przy pooling itd
/// </summary>
public class Enemy : Character
{
    
    
     #region Variables
    [DisableInEditMode]
    public EnemyType enemyType;
    
    private TankController tankController;

    public List<AbilityDataSO> abilitiesOnDie = new List<AbilityDataSO>();
    public List<AbilityDataSO> abilitiesOnHit = new List<AbilityDataSO>();
    public List<AbilityDataSO> abilitiesOnFinish = new List<AbilityDataSO>();
    
    #endregion

    private void OnEnable()
    {
        EnemyManager.Instance.RegisterEnemy(this);
        //Init();
    }

    private void OnDisable()
    {
        if (EnemyManager.Instance)
            EnemyManager.Instance.UnregisterEnemy(this);
    }

    public override void Init()
    {
        LoadData();
    }

    protected override void LoadData()
    {
        if (characterConfig == null)
            return;

        enemyType = characterConfig.enemyType;
        currentHP = characterConfig.HpMax;
        CurrentSpeed = characterConfig.SpeedMax;
    }

    //ToDO: STATS system must be created
    public override void OnSpeedChange()
    {
        base.OnSpeedChange();
        
        if (tankController == null)
            tankController = GetComponent<TankController>();

        tankController.speed = CurrentSpeed;
    }
    
    public override void TakeDamage(float incomeDamage)
    {
        base.TakeDamage(incomeDamage);
    }

    protected override void Die()
    {
        base.Die();

        EnemyManager.Instance.UnregisterEnemy(this);
        ObjectPoolManager.Instance.ReturnObject(this);
        
        foreach (var abilityDataSo in abilitiesOnDie)
            abilityDataSo.Use();
    }
    

    protected override void OnFinish()
    {
        base.OnFinish();
        foreach (var abilityDataSo in abilitiesOnFinish)
            abilityDataSo.Use();
    }

    protected override void OnHit()
    {
        base.OnHit();
        foreach (var abilityDataSo in abilitiesOnHit)
            abilityDataSo.Use();
    }

}
