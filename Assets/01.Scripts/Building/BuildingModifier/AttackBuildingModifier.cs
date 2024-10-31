using System.Collections.Generic;
using UnityEngine;

public class AttackBuildingModifier : BuildingModifier
{
    private List<Unit> _unitList;
    [SerializeField] private float _delay = 0.1f;
    private float _curTime = 0f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _radius = 1.5f;
    [SerializeField] private LayerMask _whatIsTarget;
    
    private bool FindTargets()
    {
        return Physics2D.OverlapCircle(transform.position, _radius, _whatIsTarget);
    }

    private void Fire()
    {
        
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
