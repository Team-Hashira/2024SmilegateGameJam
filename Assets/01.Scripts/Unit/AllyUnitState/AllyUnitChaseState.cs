using UnityEngine;

public class AllyUnitChaseState : AllyUnitState
{
    public AllyUnitChaseState(AllyUnit owner, AllyUnitStateMachine stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _owner.MovementCompo.SetDestination(_owner.target.position);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        float distance = Vector2.Distance(_owner.transform.position, _owner.target.position);
        if(distance > 0.5f)
        {
            _owner.MovementCompo.SetDestination(_owner.target.position);
        }

        //todo : ���ȿ� ���� ���� ���� �ȿ� ������ ����!
    }
}
