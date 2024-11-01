using System.Collections.Generic;
using Crogen.CrogenPooling;
using Crogen.HealthSystem;
using UnityEngine;

public class Building : MonoBehaviour, ISelectable
{
    [field:SerializeField] public HealthSystem healthSystem { get; private set; }
    public BuildingType buildingType;
    [SerializeField] EffectPoolType _destroyEffectPoolType;
    public List<Unit> workingUnitList;
    public int maxWorkingUnits = 5;
    private void Awake()
    {
        healthSystem.OnDieEvent += OnDie;
    }

    protected virtual void OnDie()
    {
        gameObject.Pop(_destroyEffectPoolType, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public ESeletableType SeletableType => ESeletableType.Building;

    public void Select()
    {
        if (SelectManager.Instance.GetSeletedObjects().Count == 1)
        {
            //UIManager.Instance.UnitInfomationPanelOn();
            UIManager.Instance.BulidCanvas(true);
        }
    }

    public int GetWorkingUnitsAmount()
    {
        if (workingUnitList == null) return 1;
        else
            return workingUnitList.Count + 1;
    }
    
    public void AddWorkUnit(Unit unit)
    {
        if (workingUnitList.Count < maxWorkingUnits)
        {
            workingUnitList.Add(unit);
            unit.transform.SetParent(transform);
            unit.gameObject.SetActive(false);
        }
    }

    public void RemoveWorkUnit(Unit unit)
    {
        if (workingUnitList.Contains(unit) == false) return;
        workingUnitList.Remove(unit);
        unit.transform.SetParent(null);
        unit.gameObject.SetActive(false);
    }

    public void Deselect()
    {
        UIManager.Instance.BulidCanvas(false);
    }
}
