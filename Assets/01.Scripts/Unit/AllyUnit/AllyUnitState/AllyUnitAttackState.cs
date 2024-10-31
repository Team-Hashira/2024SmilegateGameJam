using System;
using UnityEngine;

public class AllyUnitAttackState : AllyUnitState
{
    public AllyUnitAttackState(AllyUnit owner, AllyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _owner.GetCompo<UnitAttack>(true).Attack(_owner.target);
        _owner.GetCompo<UnitAttack>(true).OnAttackEndEvent += HandleOnAttackEndEvent;
    }

    private void HandleOnAttackEndEvent()
    {
        _stateMachine.ChangeState(EAllyUnitState.Chase);
    }

    public override void Exit()
    {
        _owner.GetCompo<UnitAttack>(true).OnAttackEndEvent -= HandleOnAttackEndEvent;
        base.Exit();
    }
}
