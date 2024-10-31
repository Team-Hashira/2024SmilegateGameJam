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
    private Collider2D[] _targetColliders;

    private void Awake()
    {
        _targetColliders = new Collider2D[1];
        _owner = GetComponent<Building>();

	}

    private bool FindTargets()
    {
        var contactFilter = new ContactFilter2D() { useLayerMask = true, layerMask = _whatIsTarget };
        
        return Physics2D.OverlapCircle(transform.position, _radius, contactFilter, _targetColliders) > 0;
    }

    private void Fire()
    {
        if (!FindTargets()) return;
        Laser laser = gameObject.Pop(_laserPoolType, _firePointTrm.position, Quaternion.identity) as Laser;
        laser.Attack(_targetColliders[0].transform.position, _damage * _owner.GetWorkingUnitsAmount());
    }
    
    private void Update()
    {
        _curTime += Time.deltaTime;
        if (_curTime > _delay+_owner.GetWorkingUnitsAmount())
        {
            Fire();        
            _curTime = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
