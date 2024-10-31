using UnityEngine;

public class AllyUnit : Unit, ISelectable
{
    public ESeletableType SeletableType => ESeletableType.Unit;
    public Transform target;
    public LayerMask whatIsEnemy;
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
        Debug.Log(SelectManager.Instance.GetSeletedObjects().Count);
        if (SelectManager.Instance.GetSeletedObjects().Count == 1)
        {
            UIManager.Instance.UnitManagementPanelOn(_name, _description, RendererCompo.sprite);
            Debug.Log("具具具具具具");
        }
    }

    public void Deselect()
    {
        //UIManager.Instance.UnitManagementPanelOff();
    }

    public void Move(Vector3 destination)
    {

    }
}
