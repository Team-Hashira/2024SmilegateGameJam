using UnityEngine;

public class AllyUnit : Unit, ISelectable
{
    public ESeletableType SeletableType => ESeletableType.Unit;
    public Transform target;
    public AllyUnitStateMachine StateMachine { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new AllyUnitStateMachine(this);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.MachineUpdate();
    }

    public void Select()
    {
        Debug.Log("¼±ÅÃµÊ!@!!");

    }

    public void Deselect()
    {
        // ±× ¾Æ¿ô¶óÀÎ ²ô±â
    }

    public void Move(Vector3 destination)
    {

    }
}
