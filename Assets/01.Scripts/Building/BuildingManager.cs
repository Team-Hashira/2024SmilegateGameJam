using Crogen.PowerfulInput;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
    public InputReader inputReader;

    public Transform playerCore;
    [HideInInspector] public List<Building> currentBuildingList;
    [field:SerializeField] public BuildingPrefabDataSO BuildingPrefabDataSO { get; private set; }
    [SerializeField] private SpriteRenderer _buildingGeneratePlaceUI;
    public bool PickingBuildingPlace { get; private set; }
    private BuildingType _buildingType;

    public void PickBuildingGeneratedPlace(int buildingType)
    {
        Debug.Log((BuildingType)buildingType);
		Building building = Instantiate(BuildingPrefabDataSO.buildingPrefabs[(BuildingType)buildingType], Vector3.one * 10000, Quaternion.identity);
        SpriteRenderer sp = building.transform.Find("Visual").GetComponent<SpriteRenderer>();
        _buildingGeneratePlaceUI.sprite = sp.sprite;
        _buildingGeneratePlaceUI.color = Color.red;
        inputReader.SetControlable(false);
		_buildingType = (BuildingType)buildingType;
		PickingBuildingPlace = true;
		Destroy(building.gameObject);
        UIManager.Instance.BulidCanvas(false);
        _curTime = 0;
	}

	private Building CreateBuilding(BuildingType buildingType, Vector2 position)
    {
        Building building = Instantiate(BuildingPrefabDataSO.buildingPrefabs[buildingType], position, Quaternion.identity);
		inputReader.SetControlable(true);
		PickingBuildingPlace = false;
		return building;
    }
    private float _delay = 0.1f;
    private float _curTime  = 0f;
	private void Update()
	{
        _curTime += Time.deltaTime;

		if (_curTime > _delay)
        if (PickingBuildingPlace == true)
        {
            _buildingGeneratePlaceUI.transform.position = Camera.main.ScreenToWorldPoint(Event.current.mousePosition);
			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
                Debug.Log("Create");
                CreateBuilding(_buildingType, Camera.main.ScreenToWorldPoint(Event.current.mousePosition));
			}
		}
	}
}
