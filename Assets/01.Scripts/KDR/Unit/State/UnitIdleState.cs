using DG.Tweening;
using UnityEngine;

public class UnitIdleState : State
{
    public UnitIdleState(Unit owner, EnemyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    private Sequence _seq;

    public override void Enter()
    {
        base.Enter();

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            _stateMachine.ChangeState(EEnemyUnitState.Patrol);
        }
    }
}
