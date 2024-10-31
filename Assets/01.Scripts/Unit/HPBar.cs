using Crogen.HealthSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class HPBar : MonoBehaviour
{
    private HealthSystem _healthSystem;
    [SerializeField]
    private SpriteRenderer _hpBar, _subBar;
    private Transform _hpBarTrm, _subBarTrm;
    private Sequence _hpBarSequence;

    private bool _isApplying = false;


    public void Initialize(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
        _healthSystem.OnHPChangeEvent += HandleOnHealthChangeEvent;
        _hpBarTrm = _hpBar.transform.parent;
        _subBarTrm = _subBar.transform.parent;
    }

    

    private void HandleOnHealthChangeEvent(float prev, float current)
    {
        float ratio = current / _healthSystem.maxHp;
        _hpBarTrm.localScale = new Vector2(ratio, 1);
        if (_hpBarSequence != null && _hpBarSequence.IsActive())
            _hpBarSequence.Kill();
        _hpBarSequence = DOTween.Sequence();
        if (!_isApplying)
            _hpBarSequence.AppendInterval(0.35f);
        _hpBarSequence.AppendCallback(() => _isApplying = true);
        _hpBarSequence.Append(_subBarTrm.DOScale(_hpBarTrm.localScale, 0.2f));
        _hpBarSequence.AppendCallback(() => _isApplying = false);
    }
}
