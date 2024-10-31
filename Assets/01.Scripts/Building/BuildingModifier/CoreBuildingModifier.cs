using System;
using Crogen.HealthSystem;
using UnityEngine;

public class CoreBuildingModifier : BuildingModifier
{
    private void Awake()
    {
        _owner.healthSystem.OnDieEvent += OnDie;
    }

    private void OnDie()
    {
        //이거 파괴되었을 때 게임 오버되게 해야 함.
    }
}
