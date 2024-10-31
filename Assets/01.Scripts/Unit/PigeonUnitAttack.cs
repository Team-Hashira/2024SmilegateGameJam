using DG.Tweening;
using UnityEngine;

public class PigeonUnitAttack : UnitAttack
{
    private Sequence _attackSeq;
    [SerializeField] private BirdPoop _birdPoop;
    [SerializeField] private Animator _animator;

    public override void Attack(Transform target)
    {
        if (_attackSeq != null && _attackSeq.IsActive()) _attackSeq.Kill();
        _attackSeq = DOTween.Sequence();

        _attackSeq.AppendCallback(() => _animator.SetBool("IsFly", true))
            .AppendInterval(0.6f)
            .AppendCallback(() =>
            {
                BirdPoop shell = Instantiate(_birdPoop, transform.position + Vector3.down * 0.6f, Quaternion.identity);
                shell.Init((int)_owner.Stat.GetStatValue(EStatType.Damage));
            })
            .AppendInterval(0.6f)
            .AppendCallback(() => _animator.SetBool("IsFly", false))
            .AppendCallback(() => base.Attack(target));
    }
}
