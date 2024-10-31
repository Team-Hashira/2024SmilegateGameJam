using Crogen.CrogenPooling;
using UnityEngine;

public class UnitGenerateBuildingModifier : BuildingModifier
{
    [SerializeField] private float _delay;
    private float _curTime = 0f;
    [SerializeField] private Transform _generatePointTrm;
    public UnitPoolType unitPoolType;
    public bool canGenerate = true;
    
    private void Generate()
    {
        if (canGenerate == false) return;
        gameObject.Pop(unitPoolType, _generatePointTrm.position, Quaternion.identity);
    }
    
    private void Update()
    {
        _curTime += Time.deltaTime;
        if (_curTime > _delay)
        {
            Generate();
            _curTime = 0f;
        }
    }
}
