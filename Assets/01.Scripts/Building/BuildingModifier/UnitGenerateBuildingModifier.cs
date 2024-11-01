using Crogen.CrogenPooling;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerateBuildingModifier : BuildingModifier
{
    [SerializeField] private float _delay;
    private float _curTime = 0f;
    [SerializeField] private Transform _generatePointTrm;
    public EnemyUnitPoolType unitPoolType;
    public bool canGenerate = true;

    [SerializeField] private bool _isEnemyGenerater;
    [SerializeField] private int _departUnitCount;
    private List<EnemyUnit> _waitEnemyUnitList = new List<EnemyUnit>();
    
    private void Generate()
    {
        if (canGenerate == false) return;
        Vector3 offset = Random.insideUnitCircle * 2.5f;
        Unit unit = gameObject.Pop(unitPoolType, _generatePointTrm.position + offset, Quaternion.identity) as Unit;
        if (_isEnemyGenerater)
        {
            _waitEnemyUnitList.Add(unit as EnemyUnit);
        }
    }

    private void Awake()
    {
        _owner = GetComponent<Building>();
    }

    private void Update()
    {
        _curTime += Time.deltaTime;
        if (_curTime > _delay / _owner.GetWorkingUnitsAmount())
        {
            Generate();
            _curTime = 0f;
        }

        if (_isEnemyGenerater)
        {
            for (int i = 0; i < _waitEnemyUnitList.Count; i++)
            {
                if (_waitEnemyUnitList[i].StateMachine.CurrentStateEnum == EEnemyUnitState.Chase)
                {
                    _waitEnemyUnitList.Remove(_waitEnemyUnitList[i]);
                    break;
                }
            }
            if (_waitEnemyUnitList.Count >= _departUnitCount)
            {
                _waitEnemyUnitList.ForEach(unit => unit.SetPath(BuildingManager.Instance.playerCore.position));
                _waitEnemyUnitList.Clear();
            }
        }
    }
}
