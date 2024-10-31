using System.Collections.Generic;
using System;
using UnityEngine;

public enum EAllyUnitState
{
    Idle,
    Move,
    Chase,
    Attack,
    Dead
}

public class AllyUnitStateMachine
{
    private Dictionary<EAllyUnitState, AllyUnitState> stateDictionary
        = new Dictionary<EAllyUnitState, AllyUnitState>();

    private EAllyUnitState _currentStateEnum;
    private Unit _owner;
    public AllyUnitState CurrentState
        => stateDictionary[_currentStateEnum];
    public EAllyUnitState CurrentStateEnum => _currentStateEnum;

    public AllyUnitStateMachine(Unit owner)
    {
        _owner = owner;

        foreach (EAllyUnitState stateEnum in Enum.GetValues(typeof(EAllyUnitState)))
        {
            string enumName = stateEnum.ToString();
            Type t = Type.GetType("AllyUnit" + enumName + "State");
            AllyUnitState state = Activator.CreateInstance(t, owner, this) as AllyUnitState;
            stateDictionary.Add(stateEnum, state);
        }
        CurrentState.Enter();
    }

    public void MachineUpdate()
    {
        CurrentState.StateUpdate();
    }

    public void ChangeState(EAllyUnitState newState)
    {
        if (stateDictionary.ContainsKey(newState) == false || _currentStateEnum == newState) return;

        CurrentState.Exit();
        _currentStateEnum = newState;
        CurrentState.Enter();
    }
}
