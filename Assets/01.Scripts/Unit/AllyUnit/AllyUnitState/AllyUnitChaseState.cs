using DG.Tweening;
using UnityEngine;

public class AllyUnitChaseState : AllyUnitState
{
    public AllyUnitChaseState(AllyUnit owner, AllyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
        _attackTime = owner.Stat.GetStatValue(EStatType.AttackCooltime);
        _colliders = new Collider2D[1];
    }

    private Sequence _seq;

    private float _pathfindingTimer = 0f;
    private float _pathfindingTime = 0.25f;

    private float _attackTime;
    private float _attackTimer = 0;

    private Collider2D[] _colliders;

    public override void Enter()
    {
        base.Enter();

        if(_owner.target == null) // 공격 했는데 애가 죽었네? 어머나
        {
            int count = Physics2D.OverlapCircle(_owner.transform.position, _owner.Stat.GetStatValue(EStatType.DetectRadius), new ContactFilter2D() { layerMask = _owner.whatIsEnemy, useLayerMask = true, useTriggers = true }, _colliders);
            if(count > 0)
            {
                _owner.target = _colliders[0].transform;
            }
        }
        _owner.VisualPivotTrm.localEulerAngles = new Vector3(0, 0, -7f);
        _seq = DOTween.Sequence();
        _seq.Append(_owner.VisualPivotTrm.DOLocalRotate(new Vector3(0, 0, 7f), 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
        _seq.Join(_owner.VisualPivotTrm.DOLocalMoveY(0.001f, 0.15f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo));
        _owner.GetCompo<UnitMovement>().SetDestination(_owner.target.position);
        //_owner.GetCompo<UnitMovement>().OnMoveEndEvent += HandleOnMoveEndEvent;
    }

    private void HandleOnMoveEndEvent()
    {
        _stateMachine.ChangeState(EAllyUnitState.Idle);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_owner.target == null) // 공격 하기 전에 얘가 죽었다
        {
            int count = Physics2D.OverlapCircle(_owner.transform.position, _owner.Stat.GetStatValue(EStatType.DetectRadius), new ContactFilter2D() { layerMask = _owner.whatIsEnemy, useLayerMask = true, useTriggers = true }, _colliders);
            if (count > 0)
            {
                _owner.target = _colliders[0].transform;
            }
        }
        _pathfindingTimer += Time.deltaTime;
        _attackTimer += Time.deltaTime;
        if (_pathfindingTimer > _pathfindingTime)
        {
            _owner.GetCompo<UnitMovement>().SetDestination(_owner.target.position);
            _pathfindingTimer = 0;
        }
        float distance = Vector2.Distance(_owner.target.position, _owner.transform.position);
        if (_attackTimer > _attackTime && distance < _owner.Stat.GetStatValue(EStatType.AttackRadius))
        {
            _stateMachine.ChangeState(EAllyUnitState.Attack);
            _attackTimer = 0;
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (_seq != null && _seq.IsActive()) _seq.Kill();

        _owner.VisualPivotTrm.localEulerAngles = Vector3.one;
        _owner.VisualPivotTrm.localPosition = new Vector3(0, -0.25f, 0);
        //_owner.GetCompo<UnitMovement>().OnMoveEndEvent -= HandleOnMoveEndEvent;
    }
}
