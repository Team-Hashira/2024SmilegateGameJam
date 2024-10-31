using DG.Tweening;
using UnityEngine;

public class BearUnitAttack : UnitAttack
{
    private Sequence _attackSeq;
    [SerializeField] private DamageCaster2D _damageCaster;

    public override void Attack(Vector2 direction)
    {
        _attackSeq = DOTween.Sequence();


        Vector2 position = _owner.VisualPivotTrm.position;
        _attackSeq.Append(_owner.VisualPivotTrm.DOMove(position + -direction, 0.5f).SetEase(Ease.OutCubic))
            .AppendInterval(0.2f)
            .Append(_owner.VisualPivotTrm.DOMove(position + direction * 1.5f, 0.1f).SetEase(Ease.OutBounce))
            .AppendCallback(() =>
            {
                Debug.Log("�õ�");
                _damageCaster.CastDamage((int)_owner.Stat.GetStatValue(EStatType.Damage));
            })
            .Append(_owner.VisualPivotTrm.DOLocalMove(Vector2.zero, 0.3f))
            .AppendCallback(() => base.Attack(direction));

    }
}
