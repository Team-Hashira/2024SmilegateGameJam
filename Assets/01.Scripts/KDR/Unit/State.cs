using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected StateMachine _stateMachine;
    protected Unit _owner;

    public State(Unit owner, StateMachine stateMachine)
    {
        _owner = owner;
        _stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }
    public virtual void StateUpdate()
    {

    }
    public virtual void Exit()
    {

    }
}
