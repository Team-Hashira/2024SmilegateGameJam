using DG.Tweening;
using UnityEngine;

public class SheepUnitAttack : UnitAttack
{
    private Sequence _attackSeq;
    [SerializeField] private Shell _shell;

    public override void Attack(Transform target)
    {
        _attackSeq = DOTween.Sequence();
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        Shell shell = Instantiate(_shell, transform.position + Vector3.up * 0.2f, Quaternion.identity);
        shell.Init((int)_owner.Stat.GetStatValue(EStatType.Damage), direction * 6);

        _attackSeq.Append(_owner.VisualTrm.DOScaleY(0.5f, 0.1f).SetEase(Ease.OutExpo))
            .Append(_owner.VisualTrm.DOScaleY(1, 0.5f).SetEase(Ease.InSine))
            .AppendCallback(() => base.Attack(target));

    }
}
