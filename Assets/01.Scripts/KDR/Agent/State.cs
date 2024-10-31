using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected StateMachine _stateMachine;
    protected Agent _owner;

    public State(Agent owner, StateMachine stateMachine)
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
