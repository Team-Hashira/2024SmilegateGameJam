using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    None = 0,
    
}

public class BuildingManager : MonoSingleton<BuildingManager>
{
    [HideInInspector] public List<Building> currentBuildingList;
    public BuildingPrefabDataSO BuildingPrefabDataSO { get; private set; }
    public Building CreateBuilding(BuildingType buildingType, Vector2 position)
    {
        Building building = Instantiate(BuildingPrefabDataSO.buildingPrefabs[buildingType], position, Quaternion.identity);

        return building;
    }
}
