using System;
using UnityEngine;

public class Laser : Projectile
{
    [SerializeField] private Material _material;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private DamageCaster _damageCaster;
    private readonly int _colorPropertyID = Shader.PropertyToID("_Color");
    
    private void Awake()
    {
        if(_team == TeamType.Blue)
            _material.SetColor(_colorPropertyID, Color.blue);
        else 
            _material.SetColor(_colorPropertyID, Color.red);
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
