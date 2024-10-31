using Crogen.CrogenPooling;
using UnityEngine;

public class ChickenUnitAttack : UnitAttack
{
    [SerializeField]
    private DamageCaster2D _damageCaster;
    [SerializeField]
    private EffectPoolType _chickenPopType;
    public override void Attack(Transform target)
    {
        gameObject.Pop(_chickenPopType, transform.position, Quaternion.identity);
        _damageCaster.CastDamage((int)_owner.Stat.GetStatValue(EStatType.Damage));
        _owner.Push();
        base.Attack(target);
    }
}
