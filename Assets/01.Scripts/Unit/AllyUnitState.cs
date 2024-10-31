using UnityEngine;

public class AllyUnitState
{
    protected AllyUnitStateMachine _stateMachine;
    protected AllyUnit _owner;

    public AllyUnitState(AllyUnit owner, AllyUnitStateMachine stateMachine)
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
