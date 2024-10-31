using DG.Tweening;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private DamageCaster2D _damageCaster;

    public void Init(int damage, Vector2 target)
    {
        transform.DOJump(target, 2, 1, 1.5f)
            .OnComplete(() =>
            {
                _damageCaster.CastDamage(damage);
                Destroy(gameObject);
            });
    }
}
