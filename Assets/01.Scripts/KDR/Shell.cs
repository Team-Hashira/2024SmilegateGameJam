using DG.Tweening;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private DamageCaster2D _damageCaster;

    public void Init(int damage, Vector2 target)
    {
        transform.DOJump(target, 2, 1, 1f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _damageCaster.CastDamage(damage);
                Destroy(gameObject);
            });
    }
}
