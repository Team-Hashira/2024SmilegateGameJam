using Crogen.CrogenPooling;
using Crogen.HealthSystem;
using UnityEngine;

public class Building : MonoBehaviour, ISelectable
{
    [field:SerializeField] public HealthSystem healthSystem { get; private set; }
    public BuildingType buildingType;
    [SerializeField] EffectPoolType _destroyEffectPoolType;
    
    private void Awake()
    {
        healthSystem.OnDieEvent += OnDie;
    }

    protected virtual void OnDie()
    {
        gameObject.Pop(_destroyEffectPoolType, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public ESeletableType SeletableType { get=>ESeletableType.Building; }
    public void Select()
    {
        Debug.Log(SelectManager.Instance.GetSeletedObjects().Count);
        if (SelectManager.Instance.GetSeletedObjects().Count == 1)
        {
            //UIManager.Instance.UnitInfomationPanelOn();
            UIManager.Instance.BulidCanvas(true);
        }
    }

    public void Deselect()
    {
        UIManager.Instance.BulidCanvas(false);
    }
}
