using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyUnitState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Die
}

public class EnemyUnitStateMachine
{
    private Dictionary<EEnemyUnitState, State> stateDictionary
        = new Dictionary<EEnemyUnitState, State>();

    private EEnemyUnitState _currentStateEnum;
    private Unit _owner;
    public State CurrentState
        => stateDictionary[_currentStateEnum];
    public EEnemyUnitState CurrentStateEnum => _currentStateEnum;

    public EnemyUnitStateMachine(Unit owner)
    {
        _owner = owner;

        foreach (EEnemyUnitState stateEum in Enum.GetValues(typeof(EEnemyUnitState)))
        {
            string enumName = stateEum.ToString();
            Type t = Type.GetType("Unit" + enumName + "State");
            State state = Activator.CreateInstance(t, owner, this) as State;
            stateDictionary.Add(stateEum, state);
        }
        CurrentState.Enter();
    }

    public void MachineUpdate()
    {
        CurrentState.StateUpdate();
    }

    public void ChangeState(EEnemyUnitState newState)
    {
        if (stateDictionary.ContainsKey(newState) == false) return;

        CurrentState.Exit();
        _currentStateEnum = newState;
        CurrentState.Enter();
    }
}
