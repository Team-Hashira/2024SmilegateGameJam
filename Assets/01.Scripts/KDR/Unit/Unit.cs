using Crogen.HealthSystem;
using Gondr.Astar;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    public Transform VisualPivotTrm { get; private set; }
    public Transform VisualTrm { get; private set; }
    public UnitMovement MovementCompo { get; protected set; }
    public AstarAgent AStarAgentCompo { get; protected set; }
    public Collider2D ColliderCompo { get; protected set; }
    public HealthSystem HealthSystemCompo { get; protected set; }
    public StateMachine StateMachine { get; private set; }

    [field:SerializeField] public StatSO Stat { get; private set; }

    [SerializeField] private HPBar _hpBar;

    protected Dictionary<Type, IAgentComponent> _components;


    protected virtual void Awake()
    {
        VisualPivotTrm = transform.Find("VisualPivot");
        VisualTrm = VisualPivotTrm.Find("Visual");
        _components = new Dictionary<Type, IAgentComponent>();
        AddComponentToDictionary();
        ComponentInitialize();
        ComponentAfterInit();

        StateMachine = new StateMachine(this);
        HealthSystemCompo = GetComponent<HealthSystem>();
        AStarAgentCompo = GetComponent<AstarAgent>();
        AStarAgentCompo.Initialize(this);
        MovementCompo = GetComponent<UnitMovement>();
        MovementCompo.Initalize(this);
        _hpBar.Initialize(HealthSystemCompo);
    }

    protected virtual void Update()
    {
        StateMachine.MachineUpdate();

        //if (Keyboard.current.kKey.wasPressedThisFrame)
        //{
        //    HealthSystemCompo.Hp -= 10;
        //}
    }

    private void AddComponentToDictionary()
    {
        GetComponentsInChildren<IAgentComponent>(true)
            .ToList().ForEach(compo => _components.Add(compo.GetType(), compo));
    }

    private void ComponentInitialize()
    {
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
    }

    private void ComponentAfterInit()
    {
        _components.Values.ToList().ForEach(compo => compo.AfterInit());
    }

    public T GetCompo<T>(bool isderived = false) where T : class
    {
        if (_components.TryGetValue(typeof(T), out IAgentComponent compo))
        {
            return compo as T;
        }

        if (!isderived) return default;

        Type findType = _components.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
        if (findType != null)
            return _components[findType] as T;

        return default(T);
    }

    private void OnDestroy()
    {
        _components.Values.ToList().ForEach(compo => compo.Dispose());
    }
}
