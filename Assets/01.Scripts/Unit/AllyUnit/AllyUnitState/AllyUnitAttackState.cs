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
        _owner.GetCompo<UnitAttack>().Attack((_owner.target.position - _owner.transform.position).normalized);
        _owner.GetCompo<UnitAttack>().OnAttackEndEvent += HandleOnAttackEndEvent;
    }

    private void HandleOnAttackEndEvent()
    {
        _stateMachine.ChangeState(EAllyUnitState.Chase);
    }

    public override void Exit()
    {
        _owner.GetCompo<UnitAttack>().OnAttackEndEvent -= HandleOnAttackEndEvent;
        base.Exit();
    }
}
