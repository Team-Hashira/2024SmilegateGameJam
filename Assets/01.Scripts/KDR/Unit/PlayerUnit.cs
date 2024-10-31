using UnityEngine;

public class PlayerUnit : Unit
{
    public PlayerUnitStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerUnitStateMachine(this);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.MachineUpdate();
    }
}
