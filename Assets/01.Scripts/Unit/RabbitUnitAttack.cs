using Crogen.CrogenPooling;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class RabbitUnitAttack : UnitAttack
{
    [SerializeField]
    private Transform _fireTrm;
    [SerializeField]
    private ProjectilePoolType _bullet;

    public override void Attack(Transform target)
    {
        StartCoroutine(AttackCoroutine(target));
    }

    private IEnumerator AttackCoroutine(Transform target)
    {
        Vector2 direction = target.position - _fireTrm.position;
        direction.Normalize();
        for(int i = 0; i < 3; i++)
        {
            Bullet bullet = gameObject.Pop(_bullet, _fireTrm.position, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg))
                 as Bullet;
            bullet.Fire(direction);
            yield return new WaitForSeconds(0.2f);
        }
        base.Attack(target);
    }
}
