using DG.Tweening;
using UnityEngine;

public class AllyUnitChaseState : AllyUnitState
{
    public AllyUnitChaseState(AllyUnit owner, AllyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    private Sequence _seq;

    private float _pathfindingTimer = 0f;
    private float _pathfindingTime = 0.25f;

    public override void Enter()
    {
        base.Enter();

        _owner.VisualPivotTrm.localEulerAngles = new Vector3(0, 0, -7f);
        _seq = DOTween.Sequence();
        _seq.Append(_owner.VisualPivotTrm.DOLocalRotate(new Vector3(0, 0, 7f), 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
        _seq.Join(_owner.VisualPivotTrm.DOLocalMoveY(0.001f, 0.15f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo));
        _owner.MovementCompo.SetDestination(_owner.target.position);
        _owner.MovementCompo.OnMoveEndEvent += HandleOnMoveEndEvent;
    }

    private void HandleOnMoveEndEvent()
    {
        _stateMachine.ChangeState(EAllyUnitState.Idle);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        _pathfindingTimer += Time.deltaTime;
        if (_pathfindingTimer > _pathfindingTime)
        {
            _owner.MovementCompo.SetDestination(_owner.target.position);
            _pathfindingTimer = 0;
        }
        // todo : 스탯에 따라 공격 범위 안에 들어오면 공격 하 기 기기기기긱긱
    }

    public override void Exit()
    {
        base.Exit();

        if (_seq != null && _seq.IsActive()) _seq.Kill();

        _owner.VisualPivotTrm.localEulerAngles = Vector3.one;
        _owner.VisualPivotTrm.localPosition = new Vector3(0, -0.25f, 0);
        _owner.MovementCompo.OnMoveEndEvent -= HandleOnMoveEndEvent;
    }
}
