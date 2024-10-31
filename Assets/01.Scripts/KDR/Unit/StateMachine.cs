using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUnityState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Die
}

public class StateMachine
{
    private Dictionary<EUnityState, State> stateDictionary
        = new Dictionary<EUnityState, State>();

    private EUnityState _currentStateEnum;
    private Unit _owner;
    public State CurrentState
        => stateDictionary[_currentStateEnum];
    public EUnityState CurrentStateEnum => _currentStateEnum;

    public StateMachine(Unit owner)
    {
        _owner = owner;

        foreach (EUnityState stateEum in Enum.GetValues(typeof(EUnityState)))
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

    public void ChangeState(EUnityState newState)
    {
        if (stateDictionary.ContainsKey(newState) == false) return;

        CurrentState.Exit();
        _currentStateEnum = newState;
        CurrentState.Enter();
    }
}
