using System;
using UnityEngine;

public abstract class UnitAttack : MonoBehaviour, IUnitComponent
{
    protected Unit _owner;
    public event Action OnAttackEndEvent;
    public virtual void Attack(Transform target)
    {
        OnAttackEndEvent?.Invoke();
    }

    public void Initialize(Unit owner)
    {
        _owner = owner;
    }

    public void AfterInit()
    {
    }

    public void Dispose()
    {
    }
}
