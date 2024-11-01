using Crogen.CrogenPooling;
using UnityEngine;

public class AllyUnitGenerateBuildingModifier : BuildingModifier
{
    public void UnitGeneratePanelOn()
    {
        UIManager.Instance.RegisterUnitGenerateModifier(this);
        UIManager.Instance.UnitGeneratePanelOn();
    }
    public void Generate(AllyUnitPoolType poolType)
    {
        Vector3 offset = Random.insideUnitCircle * 2.5f;
        gameObject.Pop(poolType, transform.position + offset, Quaternion.identity);
    }
}
