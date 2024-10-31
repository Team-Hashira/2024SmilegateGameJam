using System;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    None = 0,
    Ore,
    BlackSmith,
    LaserTurret,
    Ranch,
    Core
    
    
}

public class BuildingManager : MonoSingleton<BuildingManager>
{
    public Transform playerCore;
    [HideInInspector] public List<Building> currentBuildingList;
    [field:SerializeField] public BuildingPrefabDataSO BuildingPrefabDataSO { get; private set; }
    public Building CreateBuilding(BuildingType buildingType, Vector2 position)
    {
        Building building = Instantiate(BuildingPrefabDataSO.buildingPrefabs[buildingType], position, Quaternion.identity);
        return building;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBuilding(BuildingType.Ore, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
