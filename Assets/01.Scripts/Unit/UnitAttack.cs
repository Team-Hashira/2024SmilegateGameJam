using UnityEngine;
using UnityEngine.InputSystem.Android;

public abstract class UnitAttack : MonoBehaviour, IUnitComponent
{
    private Unit _owner;
    public abstract void Attack();

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
