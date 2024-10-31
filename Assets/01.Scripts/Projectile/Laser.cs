using System;
using UnityEngine;

public class Laser : Projectile
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private DamageCaster2D _damageCaster;
    private readonly int _colorPropertyID = Shader.PropertyToID("_Color");
    
    private void Awake()
    {
        if(_team == TeamType.Blue)
            _lineRenderer.material.SetColor(_colorPropertyID, Color.blue);
        else 
            _lineRenderer.material.SetColor(_colorPropertyID, Color.red);
    }

    public void Attack(Vector3 target, int damage)
    {
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
