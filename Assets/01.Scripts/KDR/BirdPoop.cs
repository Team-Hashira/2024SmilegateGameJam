using UnityEngine;

public class BirdPoop : MonoBehaviour
{
    private int _damage;
    private float _delayTime = 0.5f;
    private float _lastAttackTime;

    [SerializeField] private DamageCaster2D _caster;

    public void Init(int damage)
    {
        _damage = damage;
        _lastAttackTime = Time.time;
    }

    private void Update()
    {
        if ( _lastAttackTime + _delayTime < Time.time )
        {
            _lastAttackTime = Time.time;
            _caster.CastDamage(_damage);
        }
    }
}
