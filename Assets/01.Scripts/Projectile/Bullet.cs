using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField]
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float _speed = 20f;
    public void Fire(Vector2 direction)
    {
        Debug.Log(direction);
        _rigidbody.linearVelocity = direction * _speed;
    }
}
