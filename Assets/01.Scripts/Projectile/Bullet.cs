using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private DamageCaster2D _caster2D;
    private int _damage;

    [SerializeField]
    private float _speed = 20f;
    public void Fire(Vector2 direction, int damage)
    {
        _damage = damage;
        _rigidbody.linearVelocity = direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _caster2D.CastDamage(_damage);
        Destroy(gameObject);
    }
}
