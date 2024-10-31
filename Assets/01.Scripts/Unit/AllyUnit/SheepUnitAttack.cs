using DG.Tweening;
using UnityEngine;

public class SheepUnitAttack : UnitAttack
{
    private Sequence _attackSeq;
    [SerializeField] private DamageCaster2D _damageCaster;

    public override void Attack(Vector2 direction)
    {
        _attackSeq = DOTween.Sequence();


        float startYScale = _owner.VisualTrm.localScale.y;
        _attackSeq.Append(_owner.VisualTrm.DOScaleY(0.5f, 0.2f).SetEase(Ease.OutExpo))
            .Append(_owner.VisualPivotTrm.DOScaleY(startYScale, 1f).SetEase(Ease.InSine))
            .AppendCallback(() => base.Attack(direction));

    }
}
