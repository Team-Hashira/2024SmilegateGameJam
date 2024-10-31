using System;
using DG.Tweening;
using UnityEngine;

public class Laser : Projectile
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private DamageCaster2D _damageCaster;
    private readonly int _colorPropertyID = Shader.PropertyToID("_Color");
    private float _startWidth;
    
    private void Awake()
    {
        _startWidth = _lineRenderer.startWidth; 

        if(_team == TeamType.Blue)
            _lineRenderer.material.SetColor(_colorPropertyID, Color.blue*2);
        else 
            _lineRenderer.material.SetColor(_colorPropertyID, Color.red*2);
    }

    public void Attack(Vector3 target, int damage)
    {
        _lineRenderer.startWidth = 0;
        DOTween.To(() => _lineRenderer.startWidth, x => _lineRenderer.startWidth = x, _startWidth, _duration).SetEase(Ease.OutExpo);
        
        SetTarget(target);
        
        _damageCaster.transform.position = target;
        _damageCaster.CastDamage(damage);
    }
    
    private void SetTarget(Vector3 target)
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, target);
    }
}
