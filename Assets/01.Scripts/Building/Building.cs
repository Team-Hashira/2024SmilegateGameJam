using System;
using Crogen.CrogenPooling;
using Crogen.HealthSystem;
using UnityEngine;

public class Building : MonoBehaviour
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
}
