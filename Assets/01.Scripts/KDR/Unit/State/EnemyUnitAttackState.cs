using System;
using UnityEngine;

public class EnemyUnitAttackState : State
{
    public EnemyUnitAttackState(Unit owner, EnemyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Collider2D target;
        _owner.TargetDetected(out target);

        _owner.GetCompo<UnitAttack>().Attack((target.transform.position - _owner.transform.position).normalized);

        _owner.GetCompo<UnitAttack>().OnAttackEndEvent += HandleAttavkEndEvent;
    }

    private void HandleAttavkEndEvent()
    {
        _stateMachine.ChangeState(EEnemyUnitState.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        _owner.GetCompo<UnitAttack>().OnAttackEndEvent -= HandleAttavkEndEvent;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}
