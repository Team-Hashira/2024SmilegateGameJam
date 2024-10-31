using System;
using UnityEngine;

public class Laser : Projectile
{
    [SerializeField] private Material _material;
    [SerializeField] private LineRenderer _lineRenderer;
    private int _colorPropertyID;
    
    private void Awake()
    {
        _colorPropertyID = Shader.PropertyToID("_Color");
        
        if(_team == TeamType.Blue)
            _material.SetColor(_colorPropertyID, Color.blue);
        else 
            _material.SetColor(_colorPropertyID, Color.red);
    }

    private void SetTarget(Vector3 target)
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, target);
    }

    private void Attack(Vector3 target)
    {
        
    }
}
