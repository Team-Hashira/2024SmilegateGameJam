using DG.Tweening;
using System;
using UnityEngine;

public class AllyUnitMoveState : AllyUnitState
{
    public AllyUnitMoveState(AllyUnit owner, AllyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
    }
    private Sequence _seq;

    public override void Enter()
    {
        base.Enter();

        _owner.VisualPivotTrm.localEulerAngles = new Vector3(0, 0, -7f);
        _seq = DOTween.Sequence();
        _seq.Append(_owner.VisualPivotTrm.DOLocalRotate(new Vector3(0, 0, 7f), 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
        _seq.Join(_owner.VisualPivotTrm.DOLocalMoveY(0.001f, 0.15f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo));
        _owner.GetCompo<UnitMovement>().OnMoveEndEvent += HandleOnMoveEndEvent;
    }

    private void HandleOnMoveEndEvent()
    {
        _stateMachine.ChangeState(EAllyUnitState.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        if (_seq != null && _seq.IsActive()) _seq.Kill();

        _owner.VisualPivotTrm.localEulerAngles = Vector3.one;
        _owner.VisualPivotTrm.localPosition = new Vector3(0, -0.25f, 0);
        _owner.GetCompo<UnitMovement>().OnMoveEndEvent -= HandleOnMoveEndEvent;
    }
}
