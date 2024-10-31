using System;
using System.Collections.Generic;
using Crogen.CrogenPooling;
using UnityEngine;

public class AttackBuildingModifier : BuildingModifier
{
    private List<Unit> _unitList;
    [SerializeField] private float _delay = 0.1f;
    private float _curTime = 0f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _radius = 1.5f;
    [SerializeField] private LayerMask _whatIsTarget;
    [SerializeField] private Transform _firePointTrm;
    [SerializeField] private ProjectilePoolType _laserPoolType;
    [SerializeField] private Collider2D[] _targetColliders;

    private void Awake()
    {
        _targetColliders = new Collider2D[1];
    }

    private bool FindTargets()
    {
        var contactFilter = new ContactFilter2D() { useLayerMask = true, layerMask = _whatIsTarget };
        
        return Physics2D.OverlapCircle(transform.position, _radius, contactFilter, _targetColliders) > 0;
    }

    private void Fire()
    {
        Laser laser = gameObject.Pop(_laserPoolType, _firePointTrm.position, Quaternion.identity) as Laser;
        laser.Attack();
    }
    
    private void Update()
    {
        _curTime += Time.deltaTime;
        if (_curTime > _delay)
        {
            if (FindTargets() == true)
                Fire();        
            _curTime = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
