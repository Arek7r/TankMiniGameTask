using System;
using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [DisableInEditMode]
    public AIState aiState;
    private TankController controller;
    private Character character;
    //[SerializeField] private AbilityManager abilityManager;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<TankController>();
        
        if (character == null)
            character = GetComponent<Character>();

        character.dieEvent += OnDie;
        character.finishEvent += OnFinish;
    }

    private void OnEnable()
    {
        aiState = AIState.normal;
    }

    private void OnDestroy()
    {
        if (character)
        {
            character.dieEvent -= OnDie;
            character.finishEvent -= OnFinish;
        }
    }

    void Update()
    {
        UpdateMove();
    }

    // move forward
    private void UpdateMove()
    {
        if (aiState == AIState.dead)
            return;

        if (controller)
            controller.MoveVertical(1);
    }

    private void OnDie()
    {
        aiState = AIState.dead;
    }

    private void OnFinish()
    {
        
    }
}
