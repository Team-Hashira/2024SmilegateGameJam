using DG.Tweening;
using UnityEngine;

public class EnemyUnitIdleState : State
{
    public EnemyUnitIdleState(Unit owner, EnemyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    private Sequence _seq;
    private EnemyUnit _enemyUnit;

    public override void Enter()
    {
        base.Enter();

        _enemyUnit = _owner as EnemyUnit;

        _owner.VisualTrm.localScale = new Vector3(0.95f, 1.05f);
        _seq = DOTween.Sequence();
        _seq.Append(_owner.VisualTrm.DOScale(new Vector3(1.05f, 0.95f), 0.75f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
    }

    public override void Exit()
    {
        base.Exit();

        if (_seq != null && _seq.IsActive()) _seq.Kill();
        _owner.VisualTrm.localScale = Vector3.one;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        Collider2D target;
        if (_owner.TargetDetected(out target))
        {
            if (Vector3.Distance(_owner.transform.position, target.transform.position) >= _owner.Stat.GetStatValue(EStatType.AttackRadius))
                _stateMachine.ChangeState(EEnemyUnitState.Chase);
            else if (_owner.CanAttack())
                _stateMachine.ChangeState(EEnemyUnitState.Attack);

        }
        else if(_enemyUnit.IsCoreTargeting)
        {
            if (Vector3.Distance(_owner.transform.position, _enemyUnit.CorePos) >= _owner.Stat.GetStatValue(EStatType.AttackRadius))
                _stateMachine.ChangeState(EEnemyUnitState.Patrol);
        }
    }
}
