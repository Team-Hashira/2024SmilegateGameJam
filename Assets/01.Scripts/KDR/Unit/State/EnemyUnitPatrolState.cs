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

        _enemyUnit = _owner as EnemyUnit;

        _owner.VisualPivotTrm.localEulerAngles = new Vector3(0, 0, -7f);
        _seq = DOTween.Sequence();
        _seq.Append(_owner.VisualPivotTrm.DORotate(new Vector3(0, 0, 7f), 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
        _seq.Join(_owner.VisualPivotTrm.DOMoveY(0.001f, 0.15f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo));


        _owner.GetCompo<UnitMovement>().SetDestination(_enemyUnit.CorePos);
    }

    public override void Exit()
    {
        base.Exit();

        if (_seq != null && _seq.IsActive()) _seq.Kill();

        _owner.VisualPivotTrm.localEulerAngles = Vector3.one;
        _owner.VisualPivotTrm.localPosition = new Vector3(0, -0.25f, 0);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (Vector3.Distance(_owner.transform.position, _enemyUnit.CorePos) < 1f)
        {
            _stateMachine.ChangeState(EEnemyUnitState.Idle);
        }
    }
}
