using System;
using System.Collections;
using System.Collections.Generic;
using _AR_.Extensions;
using TriInspector;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    #region Variables

    [SerializeField,ReadOnly]
    public float currentHP;
  
    [SerializeField, ReadOnly]
    private float currentSpeed;

    [InlineEditor]
    public CharacterConfig characterConfig;
    
    public event Action hitEvent;
    public event Action dieEvent;
    public event Action finishEvent;

    public float CurrentSpeed
    {
        get => currentSpeed;
        set
        {
            currentSpeed = value;
            OnSpeedChange();
        }
    }

    #endregion

    private void OnDestroy()
    {
        hitEvent = null;
        dieEvent = null;
        finishEvent = null;
    }

    public abstract void Init();
    protected abstract void LoadData();

    public virtual void TakeDamage(float incomeDamage)
    {
        if (currentHP <= 0)
            return;

        currentHP -= incomeDamage;

        OnHit();
        
        if (currentHP <= 0)
            Die();
    }
    
    protected virtual void Die()
    {
        currentHP = 0;
        dieEvent?.Invoke();
    }

    protected virtual void OnFinish()
    {
        finishEvent?.Invoke();
    }
    protected virtual void OnHit()
    {
        hitEvent?.Invoke();
    }
    
    #region Stats
    
    public virtual void AddHP(float hp)
    {
        currentHP += hp;
        ClampHP();
    }
    
    public virtual void AddHPPercent(float percent01)
    {
        currentHP = currentHP * (1 + percent01);
        ClampHP();
    }
    
    public virtual void AddMaxHPPercent01(float percent01)
    {
        currentHP = currentHP + ( characterConfig.HpMax * percent01);
        ClampHP();
    }
    
    public virtual void AddMaxHPPercent0100(float percent0100)
    {
        currentHP = currentHP + ( characterConfig.HpMax * percent0100)/100;
        ClampHP();
    }
    
    public virtual void AddSpeed(float speed)
    {
        CurrentSpeed += speed;
        ClampSpeed();
    }
    
    public virtual void AddSpeedPercent(float percent01)
    {
        CurrentSpeed = CurrentSpeed * (1 + percent01);
        ClampSpeed();
    }
    
    public virtual void AddSpeedMaxPercent0100(float percent0100)
    {
        CurrentSpeed = CurrentSpeed + ( characterConfig.SpeedMax * percent0100)/100;
        ClampSpeed();
    }

    private void ClampHP()
    {
        if (currentHP < 0 || currentHP > characterConfig.HpMax)
            currentHP.Clamp(0, characterConfig.HpMax);
    }
    private void ClampSpeed()
    {
        if (CurrentSpeed < 0 || CurrentSpeed > characterConfig.SpeedMax)
            CurrentSpeed.Clamp(0, characterConfig.SpeedMax);
    }
    #endregion

    #region Events

    // ToDo: this is ugly, need system for manage STATS
    public virtual void OnSpeedChange()
    {}

    #endregion
    public float GetCurrentPercentHP0100()
    {
        return (currentHP / characterConfig.HpMax) *100;
    }
    
}
