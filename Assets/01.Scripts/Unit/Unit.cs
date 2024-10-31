using Crogen.HealthSystem;
using Gondr.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour, ISelectable
{
    public UnitMovement MovementCompo { get; protected set; }
    public AstarAgent AStarAgentCompo { get; protected set; }
    public Collider2D ColliderCompo { get; protected set; }
    public HealthSystem HealthSystemCompo { get; protected set; }
    public ESeletableType SeletableType { get => ESeletableType.Unit; }
    [SerializeField]
    private Transform _selectVisualizer;
    [SerializeField]
    private HPBar _hpBar;


    private void Awake()
    {
        HealthSystemCompo = GetComponent<HealthSystem>();
        AStarAgentCompo = GetComponent<AstarAgent>();
        AStarAgentCompo.Initialize(this);
        MovementCompo = GetComponent<UnitMovement>();
        MovementCompo.Initalize(this);
        _hpBar.Initialize(HealthSystemCompo);
    }

    private void Start()
    {
    }

    public void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            HealthSystemCompo.Hp -= 10;
        }
    }

    public void Select()
    {
        _selectVisualizer.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _selectVisualizer.gameObject.SetActive(false);
    }
}
