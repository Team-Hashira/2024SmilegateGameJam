using DG.Tweening;
using UnityEngine;

public class EnemyUnitPatrolState : State
{
    public EnemyUnitPatrolState(Unit owner, EnemyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    private Sequence _seq;
    private EnemyUnit _enemyUnit;

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Patrol");

        _enemyUnit = _owner as EnemyUnit;

        _owner.VisualPivotTrm.localEulerAngles = new Vector3(0, 0, -7f);
        _seq = DOTween.Sequence();
        _seq.Append(_owner.VisualPivotTrm.DOLocalRotate(new Vector3(0, 0, 7f), 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
        _seq.Join(_owner.VisualPivotTrm.DOLocalMoveY(0.001f, 0.15f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo));

        _owner.GetCompo<UnitMovement>().SetDestination(_enemyUnit.CorePos);
    }

    public override void Exit()
    {
        base.Exit();

        _owner.GetCompo<UnitMovement>().StopMove();

        if (_seq != null && _seq.IsActive()) _seq.Kill();

        _owner.VisualPivotTrm.localEulerAngles = Vector3.one;
        _owner.VisualPivotTrm.localPosition = new Vector3(0, 0, 0);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (_owner.TargetDetected())
        {
            _stateMachine.ChangeState(EEnemyUnitState.Chase);
        }
        else if (Vector3.Distance(_owner.transform.position, _enemyUnit.CorePos) < _owner.Stat.GetStatValue(EStatType.AttackRadius) &&
            _owner.CanAttack())
            _stateMachine.ChangeState(EEnemyUnitState.Attack);
    }
}
