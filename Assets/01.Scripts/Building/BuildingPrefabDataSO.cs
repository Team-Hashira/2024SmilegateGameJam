using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingPrefabDataSO", menuName = "SO/BuildingPrefabDataSO", order = 0)]
public class BuildingPrefabDataSO : ScriptableObject
{
    public SerializedDictionary<BuildingType, Building> buildingPrefabs;

    private void Reset()
    {
        BuildingType[] buildingTypes = Enum.GetValues(typeof(BuildingType)) as BuildingType[];

        for (int i = 0; i < buildingTypes.Length; ++i)
        {
            buildingPrefabs.Add(buildingTypes[i], null);
        }
    }
}
