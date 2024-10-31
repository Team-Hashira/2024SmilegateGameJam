using UnityEngine;

public class EnemyUnit : Unit
{
    private Vector3 _corePos;
    private bool _isCoreTargeting;
    public EnemyUnitStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyUnitStateMachine(this);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.MachineUpdate();

        if (Input.GetKeyDown(KeyCode.F))
        {
            StateMachine.ChangeState(EEnemyUnitState.Patrol);
        }
    }

    public void SetPath(Vector3 targetPos)
    {
        _corePos = targetPos;
        _isCoreTargeting = true;
    }
}
