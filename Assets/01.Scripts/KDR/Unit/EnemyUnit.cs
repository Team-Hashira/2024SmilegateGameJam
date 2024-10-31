using UnityEngine;

public class EnemyUnit : Unit
{
    public Vector3 CorePos { get; private set; }
    public bool IsCoreTargeting { get; private set; }
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
            SetPath(Vector3.up * 20);
        }
    }

    public void SetPath(Vector3 targetPos)
    {
        CorePos = targetPos;
        IsCoreTargeting = true;
    }
}
