using Crogen.HealthSystem;
using Gondr.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    public UnitMovement MovementCompo { get; protected set; }
    public AstarAgent AStarAgentCompo { get; protected set; }
    public Collider2D ColliderCompo { get; protected set; }
    public HealthSystem HealthSystemCompo { get; protected set; }

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

    
}
