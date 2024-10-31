using Crogen.HealthSystem;
using Gondr.Astar;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Crogen.CrogenPooling;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPoolingObject, IPointerEnterHandler, IPointerExitHandler
{
    public Transform VisualPivotTrm { get; private set; }
    public Transform VisualTrm { get; private set; }
    public SpriteRenderer RendererCompo { get; private set; }
    public Collider2D ColliderCompo { get; protected set; }

    [field:SerializeField] public StatSO Stat { get; private set; }
    public string OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    [SerializeField] private HPBar _hpBar;
    [SerializeField] private LayerMask _targetLayer;

    protected Dictionary<Type, IUnitComponent> _components;

    private float _lastAttackTime;

    protected virtual void Awake()
    {
        VisualPivotTrm = transform.Find("VisualPivot");
        VisualTrm = VisualPivotTrm.Find("Visual");
        RendererCompo = VisualTrm.GetComponent<SpriteRenderer>();
        RendererCompo.material = Instantiate(RendererCompo.material);
        _components = new Dictionary<Type, IUnitComponent>();
        AddComponentToDictionary();
        ComponentInitialize();
        ComponentAfterInit();

        _hpBar.Initialize(GetCompo<HealthSystem>(true));
    }

    private void Start()
    {

    }

    protected virtual void Update()
    {
        //if (Keyboard.current.kKey.wasPressedThisFrame)
        //{
        //    GetCompo<HealthSystem>().Hp -= 10;
        //}
    }

    public bool TargetDetected(out Collider2D targetCollider)
    {
        return targetCollider = Physics2D.OverlapCircle(transform.position, Stat.GetStatElement(EStatType.DetectRadius).GetValue(), _targetLayer);
    }
    public bool TargetDetected()
    {
        return Physics2D.OverlapCircle(transform.position, Stat.GetStatElement(EStatType.DetectRadius).GetValue(), _targetLayer);
    }

    private void AddComponentToDictionary()
    {
        GetComponentsInChildren<IUnitComponent>(true)
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

    public bool CanAttack()
    {
        return _lastAttackTime + Stat.GetStatValue(EStatType.AttackCooltime) < Time.time;
    }

    public T GetCompo<T>(bool isderived = false) where T : class
    {
        if (_components.TryGetValue(typeof(T), out IUnitComponent compo))
        {
            return compo as T;
        }

        if (!isderived) return default;

        Type findType = _components.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
        if (findType != null)
            return _components[findType] as T;

        return default(T);
    }

    public void Flip(int dir)
    {
        float yRot = dir == -1 ? 180 : 0;
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    private void OnDestroy()
    {
        _components.Values.ToList().ForEach(compo => compo.Dispose());
    }

    public void OnPop()
    {

    }

    public void OnPush()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //RendererCompo.material ¾îÂ¼±¸...
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //¿©±â¼­ ²¨ÁÖ°í
    }

}
