using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class AllyUnitIdleState : AllyUnitState
{
    public AllyUnitIdleState(AllyUnit owner, AllyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    private Tween _idleTween;

    public override void Enter()
    {
        base.Enter();

        _owner.VisualTrm.localScale = new Vector3(0.95f, 1.05f);
        _idleTween = _owner.VisualTrm.DOScale(new Vector3(1.05f, 0.95f), 0.75f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public override void Exit()
    {
        base.Exit();

        if (_idleTween != null && _idleTween.IsActive()) _idleTween.Kill();
        _owner.VisualTrm.localScale = Vector3.one;
    }
}
