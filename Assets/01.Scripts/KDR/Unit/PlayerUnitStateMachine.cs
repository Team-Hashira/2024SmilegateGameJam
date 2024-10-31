using System.Collections.Generic;
using System;
using UnityEngine;

public enum EPlayerUnitState
{
    Idle,
    Move,
    Attack,
    Die
}

public class PlayerUnitStateMachine : MonoBehaviour
{
    private Dictionary<EPlayerUnitState, State> stateDictionary
        = new Dictionary<EPlayerUnitState, State>();

    private EPlayerUnitState _currentStateEnum;
    private Unit _owner;
    public State CurrentState
        => stateDictionary[_currentStateEnum];
    public EPlayerUnitState CurrentStateEnum => _currentStateEnum;

    public PlayerUnitStateMachine(Unit owner)
    {
        _owner = owner;

        foreach (EPlayerUnitState stateEum in Enum.GetValues(typeof(EPlayerUnitState)))
        {
            string enumName = stateEum.ToString();
            Type t = Type.GetType("PlayerUnit" + enumName + "State");
            State state = Activator.CreateInstance(t, owner, this) as State;
            stateDictionary.Add(stateEum, state);
        }
        CurrentState.Enter();
    }

    public void MachineUpdate()
    {
        CurrentState.StateUpdate();
    }

    public void ChangeState(EPlayerUnitState newState)
    {
        if (stateDictionary.ContainsKey(newState) == false) return;

        CurrentState.Exit();
        _currentStateEnum = newState;
        CurrentState.Enter();
    }
}
